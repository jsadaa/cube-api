using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Entities;
using ApiCube.Domain.Exceptions;
using ApiCube.Domain.Services;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Repositories.Client;
using ApiCube.Persistence.Repositories.CommandeClient;
using ApiCube.Persistence.Repositories.FactureClient;
using ApiCube.Persistence.Repositories.PanierClient;
using ApiCube.Persistence.Repositories.Produit;
using ApiCube.Persistence.Repositories.Stock;
using AutoMapper;

namespace ApiCube.Application.Services.CommandeClient;

public class CommandeClientService : ICommandeClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly ICommandeClientRepository _commandeClientRepository;
    private readonly ApiDbContext _context;
    private readonly IFactureClientRepository _factureClientRepository;
    private readonly GestionnaireDeFacturation _gestionnaireDeFacturation;
    private readonly GestionnaireDeStock _gestionnaireDeStock;
    private readonly IMapper _mapper;
    private readonly IPanierClientRepository _panierClientRepository;
    private readonly PreparateurDeCommandeClient _preparateurDeCommandeClient;
    private readonly IProduitRepository _produitRepository;
    private readonly IStockRepository _stockRepository;

    public CommandeClientService(
        IClientRepository clientRepository,
        ICommandeClientRepository commandeClientRepository,
        IFactureClientRepository factureClientRepository,
        GestionnaireDeFacturation gestionnaireDeFacturation,
        GestionnaireDeStock gestionnaireDeStock,
        IMapper mapper,
        IPanierClientRepository panierClientRepository,
        PreparateurDeCommandeClient preparateurDeCommandeClient,
        IProduitRepository produitRepository,
        IStockRepository stockRepository,
        ApiDbContext context
    )
    {
        _clientRepository = clientRepository;
        _commandeClientRepository = commandeClientRepository;
        _factureClientRepository = factureClientRepository;
        _gestionnaireDeFacturation = gestionnaireDeFacturation;
        _gestionnaireDeStock = gestionnaireDeStock;
        _mapper = mapper;
        _panierClientRepository = panierClientRepository;
        _preparateurDeCommandeClient = preparateurDeCommandeClient;
        _produitRepository = produitRepository;
        _stockRepository = stockRepository;
        _context = context;
    }

    public BaseResponse CreerUnPanier(int idClient)
    {
        try
        {
            var client = _clientRepository.Trouver(idClient);
            var panierClient = new PanierClient(client);
            client.AjouterPanier(panierClient);

            _clientRepository.Modifier(client);

            return new BaseResponse(
                HttpStatusCode.Created,
                new { code = "panier_cree" }
            );
        }
        catch (ClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse AjouterUnProduitAuPanier(int id, ProduitPanierRequest produitPanierRequest)
    {
        try
        {
            var produit = _produitRepository.Trouver(produitPanierRequest.IdProduit);
            var panierClient = _panierClientRepository.Trouver(id);
            var lignePanierClient = new LignePanierClient(produit, produitPanierRequest.Quantite);

            panierClient.AjouterLignePanierClient(lignePanierClient);

            _panierClientRepository.Modifier(panierClient);

            return new BaseResponse(
                HttpStatusCode.Created,
                new { code = "produit_ajoute_au_panier" }
            );
        }
        catch (ProduitIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (ClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (QuantitePanierInvalide e)
        {
            var response = new BaseResponse(
                HttpStatusCode.BadRequest,
                new { code = e.Message }
            );

            return response;
        }
        catch (ProduitDejaDansPanier e)
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse TrouverUnPanier(int id)
    {
        try
        {
            var panierClient = _panierClientRepository.Trouver(id);
            var panierClientResponse = _mapper.Map<PanierClientResponse>(panierClient);

            return new BaseResponse(
                HttpStatusCode.OK,
                panierClientResponse
            );
        }
        catch (PanierClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse ListerLesPaniersDUnClient(int idClient)
    {
        try
        {
            // On vérifie que le client existe
            _ = _clientRepository.Trouver(idClient);

            var paniersClient = _panierClientRepository.ListerParClient(idClient);
            var paniersClientResponse = _mapper.Map<List<PanierClientResponse>>(paniersClient);

            return new BaseResponse(
                HttpStatusCode.OK,
                paniersClientResponse
            );
        }
        catch (ClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse ModifierLaQuantiteDUnProduitDansLePanier(int id, ProduitPanierUpdate produitPanierUpdate)
    {
        try
        {
            var panierClient = _panierClientRepository.Trouver(id);
            var lignePanierClient = panierClient.LignePanierClients
                .FirstOrDefault(lignePanierClient => lignePanierClient.Produit.Id == produitPanierUpdate.IdProduit);

            if (lignePanierClient is null) throw new ProduitIntrouvable();

            lignePanierClient.ModifierQuantite(produitPanierUpdate.Quantite);

            _panierClientRepository.Modifier(panierClient);

            return new BaseResponse(
                HttpStatusCode.OK,
                new { code = "quantite_produit_modifiee_dans_panier" }
            );
        }
        catch (ProduitIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (PanierClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (QuantitePanierInvalide e)
        {
            var response = new BaseResponse(
                HttpStatusCode.BadRequest,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse ViderUnPanier(int id)
    {
        try
        {
            var panierClient = _panierClientRepository.Trouver(id);

            // TODO: Trouver une meilleure solution
            foreach (var lignePanierClient in panierClient.LignePanierClients)
                _context.RemoveRange(_context.LignesPaniersClients.Find(lignePanierClient.Id));
            _context.SaveChanges();

            return new BaseResponse(
                HttpStatusCode.OK,
                new { code = "panier_vide" }
            );
        }
        catch (PanierClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse SupprimerUnProduitDuPanier(int id, int idProduit)
    {
        try
        {
            var panierClient = _panierClientRepository.Trouver(id);
            var lignePanierClient = panierClient.LignePanierClients
                .FirstOrDefault(lignePanierClient => lignePanierClient.Produit.Id == idProduit);

            if (lignePanierClient is null) throw new ProduitNonPresentDansPanier();

            _context.RemoveRange(_context.LignesPaniersClients.Find(lignePanierClient.Id));
            _context.SaveChanges();

            panierClient.SupprimerLignePanierClient(lignePanierClient);

            _panierClientRepository.Modifier(panierClient);

            return new BaseResponse(
                HttpStatusCode.OK,
                new { code = "produit_supprime_du_panier" }
            );
        }
        catch (ProduitIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (PanierClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (ProduitNonPresentDansPanier e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse SupprimerUnPanier(int id)
    {
        try
        {
            var panierClient = _panierClientRepository.Trouver(id);
            _panierClientRepository.Supprimer(panierClient);

            return new BaseResponse(
                HttpStatusCode.OK,
                new { code = "panier_supprime" }
            );
        }
        catch (PanierClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse ValiderUnPanier(int id)
    {
        try
        {
            var panierClient = _panierClientRepository.Trouver(id);
            var client = panierClient.Client;
            var commandeClient = _preparateurDeCommandeClient.Commande(panierClient);
            var factureClient = _gestionnaireDeFacturation.Commande(commandeClient);

            client.AjouterCommande(commandeClient);
            client.Facturer(factureClient);
            panierClient.Vider();

            _clientRepository.Modifier(client);

            // Ici, on supprime les lignes de panier client en base de données via le contexte pour éviter les problèmes de tracking
            // TODO: Trouver une meilleure solution
            _context.RemoveRange(_context.PaniersClients.Find(panierClient.Id));
            _context.SaveChanges();

            foreach (var ligneCommandeClient in commandeClient.LigneCommandeClients)
            {
                var produit = ligneCommandeClient.Produit;
                var stock = _stockRepository.TrouverParProduit(produit.Id);

                stock = _gestionnaireDeStock.Achat(stock, ligneCommandeClient);
                _stockRepository.Modifier(stock);
            }

            return new BaseResponse(
                HttpStatusCode.OK,
                new { code = "panier_valide" }
            );
        }
        catch (PanierClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (ClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (StockIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (QuantiteStockInsuffisante e)
        {
            var response = new BaseResponse(
                HttpStatusCode.BadRequest,
                new { code = e.Message }
            );

            return response;
        }
        catch (ProduitIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.BadRequest,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse TrouverUneCommande(int id)
    {
        try
        {
            var commandeClient = _commandeClientRepository.Trouver(id);
            var commandeClientResponse = _mapper.Map<CommandeClientResponse>(commandeClient);

            return new BaseResponse(
                HttpStatusCode.OK,
                commandeClientResponse
            );
        }
        catch (CommandeClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (FactureClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse ListerLesCommandesDUnClient(int idClient)
    {
        try
        {
            var commandesClient = _commandeClientRepository.ListerParClient(idClient);
            var commandesClientResponse = _mapper.Map<List<CommandeClientResponse>>(commandesClient);

            return new BaseResponse(
                HttpStatusCode.OK,
                commandesClientResponse
            );
        }
        catch (ClientIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public BaseResponse ListerLesCommandes()
    {
        try
        {
            var commandesClient = _commandeClientRepository.Lister();
            var commandesClientResponse = _mapper.Map<List<CommandeClientResponse>>(commandesClient);

            return new BaseResponse(
                HttpStatusCode.OK,
                commandesClientResponse
            );
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }
}