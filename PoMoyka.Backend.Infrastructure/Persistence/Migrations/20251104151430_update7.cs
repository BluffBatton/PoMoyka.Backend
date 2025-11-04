using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoMoyka.Backend.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class update7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_CenterServices_CenterServiceID",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_CenterServices_Centers_CenterID",
                table: "CenterServices");

            migrationBuilder.DropForeignKey(
                name: "FK_CenterServices_TypeServices_TypeServiceID",
                table: "CenterServices");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Transactions_TransactionID",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Bookings_BookingID",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeServices_Services_ServiceID",
                table: "TypeServices");

            migrationBuilder.RenameColumn(
                name: "ServiceID",
                table: "TypeServices",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_TypeServices_ServiceID",
                table: "TypeServices",
                newName: "IX_TypeServices_ServiceId");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                table: "Transactions",
                newName: "BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_BookingID",
                table: "Transactions",
                newName: "IX_Transactions_BookingId");

            migrationBuilder.RenameColumn(
                name: "TransactionID",
                table: "Ratings",
                newName: "TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_TransactionID",
                table: "Ratings",
                newName: "IX_Ratings_TransactionId");

            migrationBuilder.RenameColumn(
                name: "TypeServiceID",
                table: "CenterServices",
                newName: "TypeServiceId");

            migrationBuilder.RenameColumn(
                name: "CenterID",
                table: "CenterServices",
                newName: "CenterId");

            migrationBuilder.RenameIndex(
                name: "IX_CenterServices_TypeServiceID",
                table: "CenterServices",
                newName: "IX_CenterServices_TypeServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_CenterServices_CenterID",
                table: "CenterServices",
                newName: "IX_CenterServices_CenterId");

            migrationBuilder.RenameColumn(
                name: "CenterServiceID",
                table: "Bookings",
                newName: "CenterServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_CenterServiceID",
                table: "Bookings",
                newName: "IX_Bookings_CenterServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_CenterServices_CenterServiceId",
                table: "Bookings",
                column: "CenterServiceId",
                principalTable: "CenterServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CenterServices_Centers_CenterId",
                table: "CenterServices",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CenterServices_TypeServices_TypeServiceId",
                table: "CenterServices",
                column: "TypeServiceId",
                principalTable: "TypeServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Transactions_TransactionId",
                table: "Ratings",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Bookings_BookingId",
                table: "Transactions",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeServices_Services_ServiceId",
                table: "TypeServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_CenterServices_CenterServiceId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_CenterServices_Centers_CenterId",
                table: "CenterServices");

            migrationBuilder.DropForeignKey(
                name: "FK_CenterServices_TypeServices_TypeServiceId",
                table: "CenterServices");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Transactions_TransactionId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Bookings_BookingId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeServices_Services_ServiceId",
                table: "TypeServices");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "TypeServices",
                newName: "ServiceID");

            migrationBuilder.RenameIndex(
                name: "IX_TypeServices_ServiceId",
                table: "TypeServices",
                newName: "IX_TypeServices_ServiceID");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Transactions",
                newName: "BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_BookingId",
                table: "Transactions",
                newName: "IX_Transactions_BookingID");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Ratings",
                newName: "TransactionID");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_TransactionId",
                table: "Ratings",
                newName: "IX_Ratings_TransactionID");

            migrationBuilder.RenameColumn(
                name: "TypeServiceId",
                table: "CenterServices",
                newName: "TypeServiceID");

            migrationBuilder.RenameColumn(
                name: "CenterId",
                table: "CenterServices",
                newName: "CenterID");

            migrationBuilder.RenameIndex(
                name: "IX_CenterServices_TypeServiceId",
                table: "CenterServices",
                newName: "IX_CenterServices_TypeServiceID");

            migrationBuilder.RenameIndex(
                name: "IX_CenterServices_CenterId",
                table: "CenterServices",
                newName: "IX_CenterServices_CenterID");

            migrationBuilder.RenameColumn(
                name: "CenterServiceId",
                table: "Bookings",
                newName: "CenterServiceID");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_CenterServiceId",
                table: "Bookings",
                newName: "IX_Bookings_CenterServiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_CenterServices_CenterServiceID",
                table: "Bookings",
                column: "CenterServiceID",
                principalTable: "CenterServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CenterServices_Centers_CenterID",
                table: "CenterServices",
                column: "CenterID",
                principalTable: "Centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CenterServices_TypeServices_TypeServiceID",
                table: "CenterServices",
                column: "TypeServiceID",
                principalTable: "TypeServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Transactions_TransactionID",
                table: "Ratings",
                column: "TransactionID",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Bookings_BookingID",
                table: "Transactions",
                column: "BookingID",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeServices_Services_ServiceID",
                table: "TypeServices",
                column: "ServiceID",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
