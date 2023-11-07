using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
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

    public BaseResponse AjouterUnProduitAuCatalogue(ProduitRequestDTO produitRequestDTO)
    {
        try
        {
            var fournisseur = _fournisseurRepository.Trouver(produitRequestDTO.FournisseurId);
            var familleProduit = _familleProduitRepository.Trouver(produitRequestDTO.FamilleProduitId);

            var nouveauProduit = new Domain.Entities.Produit(
                nom: produitRequestDTO.Nom,
                description: produitRequestDTO.Description,
                appellation: produitRequestDTO.Appellation,
                cepage: produitRequestDTO.Cepage,
                region: produitRequestDTO.Region,
                degreAlcool: produitRequestDTO.DegreAlcool,
                prixAchat: produitRequestDTO.PrixAchat,
                prixVente: produitRequestDTO.PrixVente,
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
            var produits = _mapper.Map<List<ProduitResponseDTO>>(listeProduits);

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
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1452 })
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { message = "Le produit ou la promotion n'a pas été trouvé" }
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
        catch (DbUpdateException e) when (e.InnerException is MySqlException { Number: 1452 })
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.NotFound,
                data: new { message = "Le produit n'a pas été trouvé" }
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