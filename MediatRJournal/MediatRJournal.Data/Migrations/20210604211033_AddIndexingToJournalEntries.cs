using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediatRJournal.Data.Migrations
{
    public partial class AddIndexingToJournalEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Journals_JournalId",
                table: "Entries");

            migrationBuilder.AlterColumn<Guid>(
                name: "JournalId",
                table: "Entries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Entries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Journals_JournalId",
                table: "Entries",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Journals_JournalId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Entries");

            migrationBuilder.AlterColumn<Guid>(
                name: "JournalId",
                table: "Entries",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Journals_JournalId",
                table: "Entries",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
