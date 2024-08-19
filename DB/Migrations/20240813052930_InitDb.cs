using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: ["Id", "PasswordHash", "PasswordSalt", "Role", "UserName"],
                values: [1, "myERz/2cBeCVpiUrdyftFRRbMOFasrabKtXgzvpN7W0qYhnJHVvmeXh3WrOMrz+p17AJUfyIWCsHitQmi+o+yw=="u8.ToArray(),
                    "85PRl+y6QQa83/XkXOuUMiqO5gLNBQhlzQSISk3MJ2QlmsmhR4ezHjC7ztbv1rHmewCm3fcCUkkPB5yVUmj98DlIt2driV89CwjwPqQ8h4HWyPxqEWAVUUAGJlAWFHPXv9kGIMSUGkhmWbNk05FcPI9JE7G7oSMrvPXpU/lTByo="u8.ToArray(),
                    1, "Administrator"]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
