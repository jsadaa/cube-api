using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Domain.Enums.Stock;
using ApiCube.Domain.Services;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Repositories.CommandeFournisseur;
using ApiCube.Persistence.Repositories.Employe;
using ApiCube.Persistence.Repositories.Fournisseur;
using ApiCube.Persistence.Repositories.Produit;
using ApiCube.Persistence.Repositories.Stock;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ApiCube.Application.Services.CommandeFournisseur;

public class CommandeFournisseurService : ICommandeFournisseurService
{
    private readonly PreparateurDeCommande _preparateurDeCommande;
    private readonly PreparateurDeStock _preparateurDeStock;
    private readonly StatutCommandeMapper _statutCommandeMapper;
    private readonly ICommandeFournisseurRepository _commandeFournisseurRepository;
    private readonly IEmployeRepository _employeRepository;
    private readonly IProduitRepository _produitRepository;
    private readonly IFournisseurRepository _fournisseurRepository;
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;

    public CommandeFournisseurService(
        PreparateurDeCommande preparateurDeCommande,
        PreparateurDeStock preparateurDeStock,
        StatutCommandeMapper statutCommandeMapper,
        ICommandeFournisseurRepository commandeFournisseurRepository,
        IEmployeRepository employeRepository,
        IProduitRepository produitRepository,
        IFournisseurRepository fournisseurRepository,
        IStockRepository stockRepository,
        IMapper mapper
    )
    {
        _preparateurDeCommande = preparateurDeCommande;
        _preparateurDeStock = preparateurDeStock;
        _statutCommandeMapper = statutCommandeMapper;
        _commandeFournisseurRepository = commandeFournisseurRepository;
        _employeRepository = employeRepository;
        _produitRepository = produitRepository;
        _fournisseurRepository = fournisseurRepository;
        _stockRepository = stockRepository;
        _mapper = mapper;
    }

    public BaseResponse AjouterUneCommandeFournisseur(CommandeFournisseurRequest commandeFournisseurRequest)
    {
        try
        {
            var fournisseur = _fournisseurRepository.Trouver(commandeFournisseurRequest.FournisseurId);
            var employe = _employeRepository.Trouver(commandeFournisseurRequest.EmployeId);

            var nouvelleCommandeFournisseur = _preparateurDeCommande.Commande(
                fournisseur: fournisseur,
                employe: employe,
                produits: commandeFournisseurRequest.Produits
            );

            _commandeFournisseurRepository.Ajouter(nouvelleCommandeFournisseur);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.Created,
                data: new { code = "commande_fournisseur_ajoute" }
            );

            return response;

        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Conflict,
                data: new { code = "commande_fournisseur_existe_deja" }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }
    
    public BaseResponse ListerLesCommandesFournisseurs()
    {
        try
        {
            var listeDesCommandesFournisseurs = _commandeFournisseurRepository.Lister();
            var commandesFournisseurs = _mapper.Map<IEnumerable<CommandeFournisseurResponse>>(listeDesCommandesFournisseurs);
            
            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: commandesFournisseurs
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }
    
    public BaseResponse TrouverUneCommandeFournisseur(int id)
    {
        try
        {
            var commandeFournisseur = _commandeFournisseurRepository.Trouver(id);
            var commandeFournisseurResponse = _mapper.Map<CommandeFournisseurResponse>(commandeFournisseur);
            
            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: commandeFournisseurResponse
            );

            return response;
        }
        catch (CommandeFournisseurIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }
    
    public BaseResponse ModifierUneCommandeFournisseur(int id, CommandeFournisseurRequest commandeFournisseurRequest)
    {
        try
        {
            var commandeFournisseur = _commandeFournisseurRepository.Trouver(id);
            BaseResponse response;
            
            // si la commande est déjà livrée, on ne peut pas la modifier
            if (commandeFournisseur.Statut == StatutCommande.Livree)
            {
                response = new BaseResponse(
                    statusCode: HttpStatusCode.BadRequest,
                    data: new { code = "commande_fournisseur_deja_livree" }
                );

                return response;
            }
            
            var fournisseur = _fournisseurRepository.Trouver(commandeFournisseurRequest.FournisseurId);
            var employe = _employeRepository.Trouver(commandeFournisseurRequest.EmployeId);
            
            var commandeFournisseurModifiee = _preparateurDeCommande.ModificationInterne(
                commandeFournisseur: commandeFournisseur,
                produits: commandeFournisseurRequest.Produits,
                fournisseur: fournisseur,
                employe: employe
            );
            
            _commandeFournisseurRepository.Modifier(commandeFournisseurModifiee);
            
            response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: new { code = "commande_fournisseur_modifiee" }
            );
            
            return response;
        }
        catch (CommandeFournisseurIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { code = e.Message }
            );

            return response;
        }
        catch (EmployeIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.BadRequest,
                data: new { code = e.Message }
            );

            return response;
        }
        catch (FournisseurIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.BadRequest,
                data: new { code = e.Message }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Conflict,
                data: new { code = "commande_fournisseur_existe_deja" }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }
    
    public BaseResponse SupprimerUneCommandeFournisseur(int id)
    {
        try
        {
            var commandeFournisseur = _commandeFournisseurRepository.Trouver(id);
            BaseResponse response;
            
            // si la commande est déjà livrée, on ne peut pas la supprimer
            if (commandeFournisseur.Statut == StatutCommande.Livree)
            {
                response = new BaseResponse(
                    statusCode: HttpStatusCode.BadRequest,
                    data: new { code = "commande_fournisseur_deja_livree" }
                );

                return response;
            }
            
            _commandeFournisseurRepository.Supprimer(commandeFournisseur);
            
            response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: new { code = "commande_fournisseur_supprimee" }
            );
            
            return response;
        }
        catch (CommandeFournisseurIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }
    
    public BaseResponse MarquerUneCommandeFournisseurCommeLivree(int id)
    {
        try
        {
            var commandeFournisseur = _commandeFournisseurRepository.Trouver(id);
            var commandeFournisseurLivree = _preparateurDeCommande.Reception(commandeFournisseur);
            
            // Mise à jour du stock
            foreach (var ligneCommandeFournisseur in commandeFournisseurLivree.LigneCommandeFournisseurs)
            {
                var stock = _stockRepository.Trouver(ligneCommandeFournisseur.Produit.Id);
                var stockModifie = _preparateurDeStock.Achat(
                    stock: stock,
                    ligneCommandeFournisseur: ligneCommandeFournisseur
                );
                _stockRepository.Modifier(stockModifie);
            }
            
            _commandeFournisseurRepository.Modifier(commandeFournisseurLivree);
            
            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: new { code = "commande_fournisseur_marquee_comme_livree" }
            );
            
            return response;
        }
        catch (CommandeFournisseurIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { code = e.Message }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Conflict,
                data: new { code = "commande_fournisseur_existe_deja" }
            );

            return response;
        }
        catch (StockIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { code = e.Message }
            );

            return response;
        }
        catch (ProduitIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.BadRequest,
                data: new { code = e.Message }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Conflict,
                data: new { code = "commande_fournisseur_existe_deja" }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }
}