using System.Net;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Persistence.Repositories.FamilleProduit;
using ApiCube.Persistence.Repositories.Fournisseur;
using ApiCube.Persistence.Repositories.Produit;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ApiCube.Application.Services.Produit;

public class ProduitService : IProduitService
{
    private readonly IProduitRepository _produitRepository;
    private readonly IFamilleProduitRepository _familleProduitRepository;
    private readonly IFournisseurRepository _fournisseurRepository;
    private readonly IMapper _mapper;

    public ProduitService(
        IProduitRepository produitRepository,
        IFamilleProduitRepository familleProduitRepository,
        IFournisseurRepository fournisseurRepository,
        IMapper mapper
    )
    {
        _produitRepository = produitRepository;
        _familleProduitRepository = familleProduitRepository;
        _fournisseurRepository = fournisseurRepository;
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
                enPromotion: produitRequestDTO.EnPromotion,
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
}