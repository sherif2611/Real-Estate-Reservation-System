using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Migrations
{
    /// <inheritdoc />
    public partial class AddEndReservationDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReservationDate",
                table: "RealEstateReservations",
                newName: "StartReservationDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndReservationDate",
                table: "RealEstateReservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndReservationDate",
                table: "RealEstateReservations");

            migrationBuilder.RenameColumn(
                name: "StartReservationDate",
                table: "RealEstateReservations",
                newName: "ReservationDate");
        }
    }
}
