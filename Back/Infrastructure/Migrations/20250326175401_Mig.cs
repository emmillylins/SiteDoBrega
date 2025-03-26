using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(100)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(100)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(100)", nullable: true),
                    ClaimType = table.Column<string>(type: "varchar(100)", nullable: true),
                    ClaimValue = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(100)", nullable: true),
                    ClaimType = table.Column<string>(type: "varchar(100)", nullable: true),
                    ClaimValue = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(100)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(100)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "varchar(100)", nullable: true),
                    UserId = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(100)", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRole", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(100)", nullable: false),
                    Cpf = table.Column<string>(type: "varchar(14)", nullable: false),
                    DataNasc = table.Column<string>(type: "varchar(8)", nullable: true),
                    TipoUsuario = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "varchar(100)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(100)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(100)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(100)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "varchar(100)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(100)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserToken",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(100)", nullable: false),
                    Value = table.Column<string>(type: "varchar(100)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(100)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserToken", x => new { x.UserId, x.CreationDate, x.Value });
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Desc = table.Column<string>(type: "varchar(30)", nullable: false),
                    Url = table.Column<string>(type: "varchar(30)", nullable: false),
                    Img = table.Column<string>(type: "varchar(200)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Multas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroAIT = table.Column<string>(type: "varchar(50)", nullable: false),
                    DataInfracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 3, 26, 14, 54, 1, 474, DateTimeKind.Local).AddTicks(6772)),
                    CodigoInfracao = table.Column<string>(type: "varchar(50)", nullable: false),
                    DescricaoInfracao = table.Column<string>(type: "varchar(50)", nullable: false),
                    PlacaVeiculo = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Multas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faixas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(100)", nullable: true),
                    Artista = table.Column<string>(type: "varchar(100)", nullable: true),
                    Link = table.Column<string>(type: "varchar(500)", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faixas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faixas_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Faixas_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Cpf",
                table: "AspNetUsers",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faixas_CategoriaId",
                table: "Faixas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Faixas_UsuarioId",
                table: "Faixas",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRole");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaim");

            migrationBuilder.DropTable(
                name: "AspNetUserClaim");

            migrationBuilder.DropTable(
                name: "AspNetUserLogin");

            migrationBuilder.DropTable(
                name: "AspNetUserRole");

            migrationBuilder.DropTable(
                name: "AspNetUserToken");

            migrationBuilder.DropTable(
                name: "Faixas");

            migrationBuilder.DropTable(
                name: "Multas");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
