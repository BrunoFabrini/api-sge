using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api_sge.Migrations
{
    public partial class v_1_0_0_VersaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    EnderecoCodigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogradouroNumero = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.EnderecoCodigo);
                });

            migrationBuilder.CreateTable(
                name: "Mercadorias",
                columns: table => new
                {
                    MercadoriaCodigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    QuantidadeEstoque = table.Column<int>(type: "int", nullable: false),
                    IncluidoUsuarioCodigo = table.Column<long>(type: "bigint", nullable: false),
                    IncluidoDataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoUsuarioCodigo = table.Column<long>(type: "bigint", nullable: false),
                    AlteradoDataHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mercadorias", x => x.MercadoriaCodigo);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioCodigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    SenhaHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SenhaSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioCodigo);
                });

            migrationBuilder.CreateTable(
                name: "Entregas",
                columns: table => new
                {
                    EntregaCodigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Frete = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IncluidoUsuarioCodigo = table.Column<long>(type: "bigint", nullable: false),
                    IncluidoDataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlteradoUsuarioCodigo = table.Column<long>(type: "bigint", nullable: false),
                    AlteradoDataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MercadoriaCodigo = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entregas", x => x.EntregaCodigo);
                    table.ForeignKey(
                        name: "FK_Entregas_Mercadorias_MercadoriaCodigo",
                        column: x => x.MercadoriaCodigo,
                        principalTable: "Mercadorias",
                        principalColumn: "MercadoriaCodigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Localizacoes",
                columns: table => new
                {
                    LocalizacaoCodigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChegadaDataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IncluidoUsuarioCodigo = table.Column<long>(type: "bigint", nullable: false),
                    IncluidoDataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnderecoCodigo = table.Column<long>(type: "bigint", nullable: true),
                    EntregaCodigo = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacoes", x => x.LocalizacaoCodigo);
                    table.ForeignKey(
                        name: "FK_Localizacoes_Enderecos_EnderecoCodigo",
                        column: x => x.EnderecoCodigo,
                        principalTable: "Enderecos",
                        principalColumn: "EnderecoCodigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Localizacoes_Entregas_EntregaCodigo",
                        column: x => x.EntregaCodigo,
                        principalTable: "Entregas",
                        principalColumn: "EntregaCodigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Enderecos",
                columns: new[] { "EnderecoCodigo", "Cidade", "Estado", "Logradouro", "LogradouroNumero" },
                values: new object[] { 1L, "Belo Horizonte", "MG", "Rua XPTO", "1000" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioCodigo", "Admin", "Login", "SenhaHash", "SenhaSalt" },
                values: new object[] { 1L, true, "admin", new byte[] { 161, 45, 193, 209, 131, 202, 63, 63, 76, 153, 222, 130, 26, 26, 179, 201, 244, 239, 205, 18, 210, 189, 89, 36, 56, 52, 85, 222, 127, 160, 28, 224, 201, 134, 245, 129, 217, 113, 78, 131, 76, 169, 6, 231, 99, 199, 53, 130, 20, 248, 51, 193, 102, 220, 101, 33, 207, 231, 148, 233, 151, 139, 248, 130 }, new byte[] { 27, 36, 16, 0, 156, 60, 33, 248, 155, 233, 19, 71, 49, 55, 114, 93, 215, 246, 96, 38, 74, 63, 43, 5, 36, 235, 95, 110, 139, 201, 243, 145, 135, 38, 122, 183, 252, 59, 56, 235, 65, 151, 55, 168, 119, 23, 199, 143, 21, 152, 248, 12, 179, 11, 184, 67, 36, 96, 246, 88, 29, 75, 85, 238, 15, 99, 55, 27, 25, 34, 189, 183, 208, 111, 165, 100, 114, 93, 254, 185, 192, 173, 15, 224, 89, 29, 104, 232, 18, 115, 110, 6, 139, 77, 124, 50, 97, 131, 176, 15, 110, 25, 185, 107, 6, 35, 206, 225, 22, 129, 85, 12, 185, 181, 177, 70, 0, 171, 178, 155, 196, 243, 177, 171, 57, 212, 36, 107 } });

            migrationBuilder.CreateIndex(
                name: "IX_Entregas_MercadoriaCodigo",
                table: "Entregas",
                column: "MercadoriaCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Localizacoes_EnderecoCodigo",
                table: "Localizacoes",
                column: "EnderecoCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Localizacoes_EntregaCodigo",
                table: "Localizacoes",
                column: "EntregaCodigo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Localizacoes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Entregas");

            migrationBuilder.DropTable(
                name: "Mercadorias");
        }
    }
}
