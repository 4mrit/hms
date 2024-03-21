using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tenant.API.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    color_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    hex_value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.color_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    feature_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.feature_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MediaFiles",
                columns: table => new
                {
                    media_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    path = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaFiles", x => x.media_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TenantDatabases",
                columns: table => new
                {
                    database_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    conncection_string = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantDatabases", x => x.database_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Schemes",
                columns: table => new
                {
                    scheme_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PrimaryColorId = table.Column<int>(type: "int", nullable: false),
                    SecondaryColorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schemes", x => x.scheme_id);
                    table.ForeignKey(
                        name: "FK_Schemes_Colors_PrimaryColorId",
                        column: x => x.PrimaryColorId,
                        principalTable: "Colors",
                        principalColumn: "color_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schemes_Colors_SecondaryColorId",
                        column: x => x.SecondaryColorId,
                        principalTable: "Colors",
                        principalColumn: "color_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    tenant_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    media_root_path = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SchemeId = table.Column<int>(type: "int", nullable: true),
                    PrimaryDatabaseId = table.Column<int>(type: "int", nullable: false),
                    SecondaryDatabaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.tenant_id);
                    table.ForeignKey(
                        name: "FK_Tenants_Schemes_SchemeId",
                        column: x => x.SchemeId,
                        principalTable: "Schemes",
                        principalColumn: "scheme_id");
                    table.ForeignKey(
                        name: "FK_Tenants_TenantDatabases_PrimaryDatabaseId",
                        column: x => x.PrimaryDatabaseId,
                        principalTable: "TenantDatabases",
                        principalColumn: "database_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tenants_TenantDatabases_SecondaryDatabaseId",
                        column: x => x.SecondaryDatabaseId,
                        principalTable: "TenantDatabases",
                        principalColumn: "database_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FeatureHospitalTenant",
                columns: table => new
                {
                    FeaturesId = table.Column<int>(type: "int", nullable: false),
                    HospitalTenantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureHospitalTenant", x => new { x.FeaturesId, x.HospitalTenantId });
                    table.ForeignKey(
                        name: "FK_FeatureHospitalTenant_Features_FeaturesId",
                        column: x => x.FeaturesId,
                        principalTable: "Features",
                        principalColumn: "feature_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureHospitalTenant_Tenants_HospitalTenantId",
                        column: x => x.HospitalTenantId,
                        principalTable: "Tenants",
                        principalColumn: "tenant_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureHospitalTenant_HospitalTenantId",
                table: "FeatureHospitalTenant",
                column: "HospitalTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Schemes_PrimaryColorId",
                table: "Schemes",
                column: "PrimaryColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Schemes_SecondaryColorId",
                table: "Schemes",
                column: "SecondaryColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_PrimaryDatabaseId",
                table: "Tenants",
                column: "PrimaryDatabaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_SchemeId",
                table: "Tenants",
                column: "SchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_SecondaryDatabaseId",
                table: "Tenants",
                column: "SecondaryDatabaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureHospitalTenant");

            migrationBuilder.DropTable(
                name: "MediaFiles");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Schemes");

            migrationBuilder.DropTable(
                name: "TenantDatabases");

            migrationBuilder.DropTable(
                name: "Colors");
        }
    }
}
