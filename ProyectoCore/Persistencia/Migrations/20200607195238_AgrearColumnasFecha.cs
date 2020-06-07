using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class AgrearColumnasFecha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Curso_CursoId",
                table: "Instructor");

            migrationBuilder.DropIndex(
                name: "IX_Instructor_CursoId",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "Instructor");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Instructor",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Curso",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Comentario",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Curso");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Comentario");

            migrationBuilder.AddColumn<Guid>(
                name: "CursoId",
                table: "Instructor",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_CursoId",
                table: "Instructor",
                column: "CursoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Curso_CursoId",
                table: "Instructor",
                column: "CursoId",
                principalTable: "Curso",
                principalColumn: "CursoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
