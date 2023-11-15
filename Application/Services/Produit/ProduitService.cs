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
    private readonly IProduitRepository _produitRepository;
    private readonly IFamilleProduitRepository _familleProduitRepository;
    private readonly IFournisseurRepository _fournisseurRepository;
    private readonly IPromotionRepository _promotionRepository;
    private readonly IMapper _mapper;

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
                nom: produitRequest.Nom,
                description: produitRequest.Description,
                appellation: produitRequest.Appellation,
                cepage: produitRequest.Cepage,
                region: produitRequest.Region,
                annee: produitRequest.Annee,
                degreAlcool: produitRequest.DegreAlcool,
                prixAchat: produitRequest.PrixAchat,
                prixVente: produitRequest.PrixVente,
                enPromotion: false,
                fournisseur: fournisseur,
                familleProduit: familleProduit
            );
            _produitRepository.Ajouter(nouveauProduit);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.Created,
                data: new { message = "Produit ajouté au stock avec succès" }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Conflict,
                data: new { message = "Ce produit existe déjà dans le stock" }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
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
                statusCode: HttpStatusCode.OK,
                data: produits
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
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
                statusCode: HttpStatusCode.OK,
                data: new { message = "Promotion appliquée avec succès" }
            );

            return response;
        }
        catch (ProduitIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { message = e.Message }
            );

            return response;
        }
        catch (PromotionIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { message = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
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
                statusCode: HttpStatusCode.OK,
                data: new { message = "Promotion supprimée avec succès" }
            );

            return response;
        }
        catch (ProduitIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { message = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
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
                statusCode: HttpStatusCode.OK,
                data: produitResponseDTO
            );

            return response;
        }
        catch (ProduitIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { message = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
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
                nom: produitUpdate.Nom,
                description: produitUpdate.Description,
                appellation: produitUpdate.Appellation,
                cepage: produitUpdate.Cepage,
                region: produitUpdate.Region,
                annee: produitUpdate.Annee,
                degreAlcool: produitUpdate.DegreAlcool,
                prixAchat: produitUpdate.PrixAchat,
                prixVente: produitUpdate.PrixVente,
                enPromotion: produitUpdate.EnPromotion
            );
            _produitRepository.Modifier(produit);

            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: new { message = "Produit modifié avec succès" }
            );

            return response;
        }
        catch (ProduitIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { message = e.Message }
            );

            return response;
        }
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1062 })
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Conflict,
                data: new { message = "Ce produit existe déjà dans le stock" }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
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
                statusCode: HttpStatusCode.OK,
                data: new { message = "Produit supprimé avec succès" }
            );

            return response;
        }
        catch (ProduitIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { message = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
            );

            return response;
        }
    }
}