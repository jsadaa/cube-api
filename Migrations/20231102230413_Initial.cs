using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCube.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nom = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prenom = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    adresse = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telephone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_naissance = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    date_inscription = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    statut = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    login = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    solde = table.Column<double>(type: "double", nullable: false),
                    points_fidelite = table.Column<int>(type: "int", nullable: false),
                    role = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "famille_produit",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nom = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_famille_produit", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "fournisseur",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nom = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    adresse = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telephone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fournisseur", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nom = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "employe",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nom = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prenom = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    adresse = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telephone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_embauche = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    date_depart = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    statut = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    salaire = table.Column<double>(type: "double", nullable: false),
                    login = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mot_de_passe = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employe", x => x.id);
                    table.ForeignKey(
                        name: "FK_employe_role_RoleModelId",
                        column: x => x.RoleModelId,
                        principalTable: "role",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "commande_client",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date_commande = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    date_livraison = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    statut = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    employe_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commande_client", x => x.id);
                    table.ForeignKey(
                        name: "FK_commande_client_client_client_id",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_commande_client_employe_employe_id",
                        column: x => x.employe_id,
                        principalTable: "employe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "commande_fournisseur",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date_commande = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    date_livraison = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    statut = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fournisseur_id = table.Column<int>(type: "int", nullable: false),
                    employe_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commande_fournisseur", x => x.id);
                    table.ForeignKey(
                        name: "FK_commande_fournisseur_employe_employe_id",
                        column: x => x.employe_id,
                        principalTable: "employe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_commande_fournisseur_fournisseur_fournisseur_id",
                        column: x => x.fournisseur_id,
                        principalTable: "fournisseur",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "facture_client",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date_facture = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    statut = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prix_ht = table.Column<double>(type: "double", nullable: false),
                    prix_ttc = table.Column<double>(type: "double", nullable: false),
                    tva = table.Column<double>(type: "double", nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    employe_id = table.Column<int>(type: "int", nullable: false),
                    commande_client_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facture_client", x => x.id);
                    table.ForeignKey(
                        name: "FK_facture_client_client_client_id",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_facture_client_commande_client_commande_client_id",
                        column: x => x.commande_client_id,
                        principalTable: "commande_client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_facture_client_employe_employe_id",
                        column: x => x.employe_id,
                        principalTable: "employe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "facture_fournisseur",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date_facture = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    date_echeance = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    statut = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prix_ht = table.Column<double>(type: "double", nullable: false),
                    prix_ttc = table.Column<double>(type: "double", nullable: false),
                    tva = table.Column<double>(type: "double", nullable: false),
                    fournisseur_id = table.Column<int>(type: "int", nullable: false),
                    employe_id = table.Column<int>(type: "int", nullable: false),
                    commande_fournisseur_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facture_fournisseur", x => x.id);
                    table.ForeignKey(
                        name: "FK_facture_fournisseur_commande_fournisseur_commande_fournisseu~",
                        column: x => x.commande_fournisseur_id,
                        principalTable: "commande_fournisseur",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_facture_fournisseur_employe_employe_id",
                        column: x => x.employe_id,
                        principalTable: "employe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_facture_fournisseur_fournisseur_fournisseur_id",
                        column: x => x.fournisseur_id,
                        principalTable: "fournisseur",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ligne_commande_client",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    quantite = table.Column<int>(type: "int", nullable: false),
                    prix_unitaire = table.Column<double>(type: "double", nullable: false),
                    remise = table.Column<double>(type: "double", nullable: false),
                    total = table.Column<double>(type: "double", nullable: false),
                    produit_id = table.Column<int>(type: "int", nullable: false),
                    commande_client_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ligne_commande_client", x => x.id);
                    table.ForeignKey(
                        name: "FK_ligne_commande_client_commande_client_commande_client_id",
                        column: x => x.commande_client_id,
                        principalTable: "commande_client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ligne_commande_fournisseur",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    quantite = table.Column<int>(type: "int", nullable: false),
                    prix_unitaire = table.Column<double>(type: "double", nullable: false),
                    remise = table.Column<double>(type: "double", nullable: false),
                    total = table.Column<double>(type: "double", nullable: false),
                    produit_id = table.Column<int>(type: "int", nullable: false),
                    commande_fournisseur_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ligne_commande_fournisseur", x => x.id);
                    table.ForeignKey(
                        name: "FK_ligne_commande_fournisseur_commande_fournisseur_commande_fou~",
                        column: x => x.commande_fournisseur_id,
                        principalTable: "commande_fournisseur",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "produit",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nom = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quantite = table.Column<int>(type: "int", nullable: false),
                    seuil_disponibilite = table.Column<int>(type: "int", nullable: false),
                    statut_stock = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prix_achat = table.Column<double>(type: "double", nullable: false),
                    prix_vente = table.Column<double>(type: "double", nullable: false),
                    date_achat = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    date_peremption = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    promotion_id = table.Column<int>(type: "int", nullable: true),
                    famille_produit_id = table.Column<int>(type: "int", nullable: false),
                    fournisseur_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produit", x => x.id);
                    table.ForeignKey(
                        name: "FK_produit_famille_produit_famille_produit_id",
                        column: x => x.famille_produit_id,
                        principalTable: "famille_produit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_produit_fournisseur_fournisseur_id",
                        column: x => x.fournisseur_id,
                        principalTable: "fournisseur",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "promotion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nom = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_debut = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    date_fin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    pourcentage = table.Column<double>(type: "double", nullable: false),
                    produit_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promotion", x => x.id);
                    table.ForeignKey(
                        name: "FK_promotion_produit_produit_id",
                        column: x => x.produit_id,
                        principalTable: "produit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "transaction_stock",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    quantite = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    produit_id = table.Column<int>(type: "int", nullable: false),
                    prix_unitaire = table.Column<double>(type: "double", nullable: false),
                    prix_total = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction_stock", x => x.id);
                    table.ForeignKey(
                        name: "FK_transaction_stock_produit_produit_id",
                        column: x => x.produit_id,
                        principalTable: "produit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_commande_client_client_id",
                table: "commande_client",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_commande_client_employe_id",
                table: "commande_client",
                column: "employe_id");

            migrationBuilder.CreateIndex(
                name: "IX_commande_fournisseur_employe_id",
                table: "commande_fournisseur",
                column: "employe_id");

            migrationBuilder.CreateIndex(
                name: "IX_commande_fournisseur_fournisseur_id",
                table: "commande_fournisseur",
                column: "fournisseur_id");

            migrationBuilder.CreateIndex(
                name: "IX_employe_RoleModelId",
                table: "employe",
                column: "RoleModelId");

            migrationBuilder.CreateIndex(
                name: "IX_facture_client_client_id",
                table: "facture_client",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_facture_client_commande_client_id",
                table: "facture_client",
                column: "commande_client_id");

            migrationBuilder.CreateIndex(
                name: "IX_facture_client_employe_id",
                table: "facture_client",
                column: "employe_id");

            migrationBuilder.CreateIndex(
                name: "IX_facture_fournisseur_commande_fournisseur_id",
                table: "facture_fournisseur",
                column: "commande_fournisseur_id");

            migrationBuilder.CreateIndex(
                name: "IX_facture_fournisseur_employe_id",
                table: "facture_fournisseur",
                column: "employe_id");

            migrationBuilder.CreateIndex(
                name: "IX_facture_fournisseur_fournisseur_id",
                table: "facture_fournisseur",
                column: "fournisseur_id");

            migrationBuilder.CreateIndex(
                name: "IX_ligne_commande_client_commande_client_id",
                table: "ligne_commande_client",
                column: "commande_client_id");

            migrationBuilder.CreateIndex(
                name: "IX_ligne_commande_client_produit_id",
                table: "ligne_commande_client",
                column: "produit_id");

            migrationBuilder.CreateIndex(
                name: "IX_ligne_commande_fournisseur_commande_fournisseur_id",
                table: "ligne_commande_fournisseur",
                column: "commande_fournisseur_id");

            migrationBuilder.CreateIndex(
                name: "IX_ligne_commande_fournisseur_produit_id",
                table: "ligne_commande_fournisseur",
                column: "produit_id");

            migrationBuilder.CreateIndex(
                name: "IX_produit_famille_produit_id",
                table: "produit",
                column: "famille_produit_id");

            migrationBuilder.CreateIndex(
                name: "IX_produit_fournisseur_id",
                table: "produit",
                column: "fournisseur_id");

            migrationBuilder.CreateIndex(
                name: "IX_produit_promotion_id",
                table: "produit",
                column: "promotion_id");

            migrationBuilder.CreateIndex(
                name: "IX_promotion_produit_id",
                table: "promotion",
                column: "produit_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_stock_produit_id",
                table: "transaction_stock",
                column: "produit_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ligne_commande_client_produit_produit_id",
                table: "ligne_commande_client",
                column: "produit_id",
                principalTable: "produit",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ligne_commande_fournisseur_produit_produit_id",
                table: "ligne_commande_fournisseur",
                column: "produit_id",
                principalTable: "produit",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_produit_promotion_promotion_id",
                table: "produit",
                column: "promotion_id",
                principalTable: "promotion",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_produit_fournisseur_fournisseur_id",
                table: "produit");

            migrationBuilder.DropForeignKey(
                name: "FK_promotion_produit_produit_id",
                table: "promotion");

            migrationBuilder.DropTable(
                name: "facture_client");

            migrationBuilder.DropTable(
                name: "facture_fournisseur");

            migrationBuilder.DropTable(
                name: "ligne_commande_client");

            migrationBuilder.DropTable(
                name: "ligne_commande_fournisseur");

            migrationBuilder.DropTable(
                name: "transaction_stock");

            migrationBuilder.DropTable(
                name: "commande_client");

            migrationBuilder.DropTable(
                name: "commande_fournisseur");

            migrationBuilder.DropTable(
                name: "client");

            migrationBuilder.DropTable(
                name: "employe");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "fournisseur");

            migrationBuilder.DropTable(
                name: "produit");

            migrationBuilder.DropTable(
                name: "famille_produit");

            migrationBuilder.DropTable(
                name: "promotion");
        }
    }
}
