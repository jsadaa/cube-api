using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests.Produit;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Persistence.Exceptions;
using ApiCube.Persistence.Repositories.FamilleProduit;
using ApiCube.Persistence.Repositories.Fournisseur;
using ApiCube.Persistence.Repositories.Produit;
using ApiCube.Persistence.Repositories.Promotion;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ApiCube.Application.Services.Produit;

public class ProduitService : IProduitService
{
    private readonly IFamilleProduitRepository _familleProduitRepository;
    private readonly IFournisseurRepository _fournisseurRepository;
    private readonly IMapper _mapper;
    private readonly IProduitRepository _produitRepository;
    private readonly IPromotionRepository _promotionRepository;

    public ProduitService(
        IProduitRepository produitRepository,
        IFamilleProduitRepository familleProduitRepository,
        IFournisseurRepository fournisseurRepository,
        IPromotionRepository promotionRepository,
        IMapper mapper
    )
    {
        _produitRepository = produitRepository;
        _familleProduitRepository = familleProduitRepository;
        _fournisseurRepository = fournisseurRepository;
        _promotionRepository = promotionRepository;
        _mapper = mapper;
    }

    public BaseResponse AjouterUnProduitAuCatalogue(ProduitRequest produitRequest)
    {
        try
        {
            var fournisseur = _fournisseurRepository.Trouver(produitRequest.FournisseurId);
            var familleProduit = _familleProduitRepository.Trouver(produitRequest.FamilleProduitId);

            var nouveauProduit = new Domain.Entities.Produit(
                produitRequest.Nom,
                produitRequest.Description,
                produitRequest.Appellation,
                produitRequest.Cepage,
                produitRequest.Region,
                produitRequest.Annee,
                produitRequest.DegreAlcool,
                prixAchat: produitRequest.PrixAchat,
                prixVente: produitRequest.PrixVente,
                datePeremption: produitRequest.DatePeremption,
                enPromotion: false,
                fournisseur: fournisseur,
                familleProduit: familleProduit
            );
            _produitRepository.Ajouter(nouveauProduit);

            var response = new BaseResponse(
                HttpStatusCode.Created,
                new { code = "produit_ajoute" }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
                new { code = "produit_existe_deja" }
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

    public BaseResponse ListerLesProduits()
    {
        try
        {
            var listeProduits = _produitRepository.Lister();
            var produits = _mapper.Map<List<ProduitResponse>>(listeProduits);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                produits
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

    public BaseResponse AppliquerUnePromotion(int produitId, int promotionId)
    {
        try
        {
            var produit = _produitRepository.Trouver(produitId);
            var promotion = _promotionRepository.Trouver(promotionId);

            produit.AppliquerPromotion(promotion);
            _produitRepository.Modifier(produit);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                new { code = "promotion_applique" }
            );

            return response;
        }
        catch (ProduitIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (PromotionIntrouvable e)
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

    public BaseResponse RetirerUnePromotion(int produitId)
    {
        try
        {
            var produit = _produitRepository.Trouver(produitId);

            produit.SupprimerPromotion();
            _produitRepository.Modifier(produit);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                new { code = "promotion_retiree" }
            );

            return response;
        }
        catch (ProduitIntrouvable e)
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

    public BaseResponse TrouverUnProduit(int produitId)
    {
        try
        {
            var produit = _produitRepository.Trouver(produitId);
            var produitResponseDTO = _mapper.Map<ProduitResponse>(produit);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                produitResponseDTO
            );

            return response;
        }
        catch (ProduitIntrouvable e)
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

    public BaseResponse ModifierUnProduit(int produitId, ProduitUpdate produitUpdate)
    {
        try
        {
            var produit = _produitRepository.Trouver(produitId);

            produit.MettreAJour(
                produitUpdate.Nom,
                produitUpdate.Description,
                produitUpdate.Appellation,
                produitUpdate.Cepage,
                produitUpdate.Region,
                produitUpdate.Annee,
                produitUpdate.DegreAlcool,
                produitUpdate.PrixAchat,
                produitUpdate.PrixVente,
                produitUpdate.DatePeremption,
                produitUpdate.EnPromotion
            );
            _produitRepository.Modifier(produit);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                new { code = "produit_modifie" }
            );

            return response;
        }
        catch (ProduitIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                HttpStatusCode.Conflict,
                new { code = "produit_existe_deja" }
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

    public BaseResponse SupprimerUnProduit(int produitId)
    {
        try
        {
            var produit = _produitRepository.Trouver(produitId);
            _produitRepository.Supprimer(produit);

            var response = new BaseResponse(
                HttpStatusCode.OK,
                new { code = "produit_supprime" }
            );

            return response;
        }
        catch (ProduitIntrouvable e)
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
}