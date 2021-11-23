using Microsoft.EntityFrameworkCore.Migrations;

namespace ZeroHunger.Migrations
{
    public partial class addReceiverToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalaryGroup",
                columns: table => new
                {
                    salaryGroupID = table.Column<int>(type: "int", nullable: false),
                    salaryRange = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryGroup", x => x.salaryGroupID);
                });

            migrationBuilder.CreateTable(
                name: "Receiver",
                columns: table => new
                {
                    receiverIC = table.Column<string>(type: "VARCHAR (12)", nullable: false),
                    receiverName = table.Column<string>(type: "VARCHAR (255)", nullable: false),
                    receiverGender = table.Column<string>(type: "VARCHAR (1)", nullable: false),
                    receiverDOB = table.Column<string>(type: "VARCHAR (10)", nullable: false),
                    receiverOccupation = table.Column<string>(type: "VARCHAR (255)", nullable: false),
                    receiverSalaryGroupID = table.Column<int>(type: "int", nullable: false),
                    receiverFamilyNo = table.Column<int>(type: "int", nullable: false),
                    receiverEmail = table.Column<string>(type: "VARCHAR (50)", nullable: false),
                    receiverPhone = table.Column<string>(type: "VARCHAR (50)", nullable: false),
                    receiverAdrs1 = table.Column<string>(type: "VARCHAR (255)", nullable: false),
                    receiverAdrs2 = table.Column<string>(type: "VARCHAR (255)", nullable: false),
                    applicationStatusID = table.Column<int>(type: "int", nullable: false,defaultValue:0),
                    userID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receiver", x => x.receiverIC);
                
                
                    table.ForeignKey(
                        name: "FK_Receiver_ToSalaryGroup",
                        column: x => x.receiverSalaryGroupID,
                        principalTable: "SalaryGroup",
                        principalColumn: "salaryGroupID"
                        );
                
                    table.ForeignKey(
                        name: "FK_Receiver_ToUser",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID"
                        );
                });

            migrationBuilder.CreateTable(
                name: "ReceiverFamily",
                columns: table => new
                {
                    familyIC = table.Column<string>(type: "VARCHAR (12)", nullable: false),
                    receiverIC = table.Column<string>(type: "VARCHAR (12)", nullable: false),
                    familyDOB = table.Column<string>(type: "VARCHAR (10)", nullable: false),
                    familyOccupation = table.Column<string>(type: "VARCHAR (50)", nullable: false),
                    familySalaryGroupID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiverFamily", x => x.familyIC);

                    table.ForeignKey(
                        name: "FK_ReceiverFamily_ToReceiver",
                        column: x => x.receiverIC,
                        principalTable: "Receiver",
                        principalColumn: "receiverIC"
                        );

                    table.ForeignKey(
                        name: "FK_ReceiverFamily_SalaryGroup",
                        column: x => x.familySalaryGroupID,
                        principalTable: "SalaryGroup",
                        principalColumn: "salaryGroupID"
                        );
                });

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Receiver");

            migrationBuilder.DropTable(
                name: "ReceiverFamily");

            migrationBuilder.DropTable(
                name: "SalaryGroup");
        }
    }
}
