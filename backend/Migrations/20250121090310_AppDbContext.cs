using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NextCore.Migrations
{
    /// <inheritdoc />
    public partial class AppDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    authorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    firstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    authorEmail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    authorPhone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.authorId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    bookId = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    datePublished = table.Column<DateOnly>(type: "date", nullable: false),
                    totalPage = table.Column<int>(type: "int", nullable: false),
                    country = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    language = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mediaType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.bookId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    cartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.cartId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    genreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    genreName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.genreId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    publisherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    publisherName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    publisherEmail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    publisherPhone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.publisherId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    firstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userEmail = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userPhone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    imageKtpPath = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Authorships",
                columns: table => new
                {
                    authorshipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    authorId = table.Column<int>(type: "int", nullable: false),
                    bookId = table.Column<string>(type: "varchar(13)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorships", x => x.authorshipId);
                    table.ForeignKey(
                        name: "FK_Authorships_Authors_authorId",
                        column: x => x.authorId,
                        principalTable: "Authors",
                        principalColumn: "authorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Authorships_Books_bookId",
                        column: x => x.bookId,
                        principalTable: "Books",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BookCopies",
                columns: table => new
                {
                    copyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    bookId = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCopies", x => x.copyId);
                    table.ForeignKey(
                        name: "FK_BookCopies_Books_bookId",
                        column: x => x.bookId,
                        principalTable: "Books",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CartDetails",
                columns: table => new
                {
                    cartId = table.Column<int>(type: "int", nullable: false),
                    bookId = table.Column<string>(type: "varchar(13)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetails", x => new { x.cartId, x.bookId });
                    table.ForeignKey(
                        name: "FK_CartDetails_Books_bookId",
                        column: x => x.bookId,
                        principalTable: "Books",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartDetails_Carts_cartId",
                        column: x => x.cartId,
                        principalTable: "Carts",
                        principalColumn: "cartId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BookGenres",
                columns: table => new
                {
                    bookId = table.Column<string>(type: "varchar(13)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    genreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenres", x => new { x.bookId, x.genreId });
                    table.ForeignKey(
                        name: "FK_BookGenres_Books_bookId",
                        column: x => x.bookId,
                        principalTable: "Books",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGenres_Genres_genreId",
                        column: x => x.genreId,
                        principalTable: "Genres",
                        principalColumn: "genreId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BooksPublished",
                columns: table => new
                {
                    bookPublishedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    publisherId = table.Column<int>(type: "int", nullable: false),
                    bookId = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksPublished", x => x.bookPublishedId);
                    table.ForeignKey(
                        name: "FK_BooksPublished_Books_bookId",
                        column: x => x.bookId,
                        principalTable: "Books",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksPublished_Publishers_publisherId",
                        column: x => x.publisherId,
                        principalTable: "Publishers",
                        principalColumn: "publisherId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    provider = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    providerAccountId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    accessToken = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    refreshToken = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tokenType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    expiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    scope = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userId = table.Column<string>(type: "varchar(16)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Borrows",
                columns: table => new
                {
                    borrowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<string>(type: "varchar(16)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    borrowDate = table.Column<DateOnly>(type: "date", nullable: true),
                    returnDate = table.Column<DateOnly>(type: "date", nullable: true),
                    duration = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrows", x => x.borrowId);
                    table.ForeignKey(
                        name: "FK_Borrows_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sessionToken = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    expires = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    userId = table.Column<string>(type: "varchar(16)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BorrowedBooks",
                columns: table => new
                {
                    borrowId = table.Column<int>(type: "int", nullable: false),
                    copyId = table.Column<int>(type: "int", nullable: false),
                    returnDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowedBooks", x => new { x.copyId, x.borrowId });
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_BookCopies_copyId",
                        column: x => x.copyId,
                        principalTable: "BookCopies",
                        principalColumn: "copyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_Borrows_borrowId",
                        column: x => x.borrowId,
                        principalTable: "Borrows",
                        principalColumn: "borrowId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "authorId", "authorEmail", "authorPhone", "firstName", "lastName" },
                values: new object[,]
                {
                    { 1, "leilachudori@gmail.com", "089685940123", "Leila S.", "Chudori" },
                    { 2, "chungserang@gmail.com", "087524691239", "Chung", "Serang" },
                    { 3, "josteingaarder@gmail.com", "085689959959", "Jostein", "Gaarder" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "bookId", "country", "datePublished", "description", "image", "language", "mediaType", "title", "totalPage" },
                values: new object[,]
                {
                    { "9786024246945", null, new DateOnly(2017, 12, 21), "Laut Bercerita, novel terbaru Leila S. Chudori, bertutur tentang kisah keluarga yang kehilangan, sekumpulan sahabat yang merasakan kekosongan di dada, sekelompok orang yang gemar menyiksa dan lancar berkhianat, sejumlah keluarga yang mencari kejelasan makam anaknya, dan tentang cinta yang tak akan luntur.", "", null, "[0, 0, 0]", "Laut Bercerita", 400 },
                    { "9786024410209", null, new DateOnly(2020, 6, 4), "Sophie, seorang pelajar sekolah menengah berusia empat belas tahun. Suatu hari sepulang sekolah, dia mendapat sebuah surat misterius yang hanya berisikan satu pertanyaan: “Siapa kamu?” Belum habis keheranannya, pada hari yang sama dia mendapat surat lain yang bertanya: “Dari manakah datangnya dunia?” Seakan tersentak dari rutinitas hidup sehari-hari, surat-surat itu membuat Sophie mulai mempertanyakan soal-soal mendasar yang tak pernah dipikirkannya selama ini. Dia mulai belajar filsafat.", "", null, "[0, 0, 0]", "Dunia Sophie", 798 },
                    { "9786231864482", null, new DateOnly(2025, 1, 9), "“Kita harus mengadakan jesa untuk Ibu.” Masalahnya, ibu mereka, Shim Sisun sang seniman kontroversial, benci jesa—upacara perkabungan khas Korea. Baginya, upacara itu hanya membebani kaum perempuan sebagai “panitia abadi” jesa. Sepuluh tahun setelah kematian Sisun, putri sulungnya mengusulkan jesa untuk ibunya di Hawaii, perantauan pertama Sisun. Bedanya, sesajen jesa akan diganti dengan benda-benda yang paling mengingatkan setiap mereka kepada Sisun. Benda apa yang mereka persembahkan? Bagaimana cerita di balik benda-benda itu? Atau... siapa sebenarnya Shim Sisun?", "", null, "[0, 0, 0]", "Dunia Sisun", 372 }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "cartId", "userId" },
                values: new object[,]
                {
                    { 1, "337502220000004" },
                    { 2, "337504440000002" },
                    { 3, "447502220000003" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "genreId", "genreName" },
                values: new object[,]
                {
                    { 1, "Fiksi" },
                    { 2, "Sejarah" },
                    { 3, "Novel" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "publisherId", "publisherEmail", "publisherName", "publisherPhone" },
                values: new object[,]
                {
                    { 1, "gramedia@gmail.com", "Kepustakaan Populer Gramedia", null },
                    { 2, "bentang@gmail.com", "Bentang Pustaka", null },
                    { 3, "mizan@gmail.com", "Mizan Publishing", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "userId", "firstName", "imageKtpPath", "lastName", "password", "userEmail", "userPhone" },
                values: new object[,]
                {
                    { "337502220000004", "Aldisar", "", "Gibran", null, "aldisarg@gmail.com", "085173043375" },
                    { "337504440000002", "Gibran", "", null, null, "gibranaldisar@gmail.com", "089685555555" },
                    { "447502220000003", "Aldisar", "", null, null, "alcendol@gmail.com", "085173045595" }
                });

            migrationBuilder.InsertData(
                table: "Authorships",
                columns: new[] { "authorshipId", "authorId", "bookId" },
                values: new object[,]
                {
                    { 1, 1, "9786024246945" },
                    { 2, 2, "9786231864482" },
                    { 3, 3, "9786024410209" }
                });

            migrationBuilder.InsertData(
                table: "BookCopies",
                columns: new[] { "copyId", "bookId", "status" },
                values: new object[,]
                {
                    { 1, "9786024246945", "Borrowed" },
                    { 2, "9786024246945", "Pending" },
                    { 3, "9786024246945", "Available" },
                    { 4, "9786024246945", "Available" },
                    { 5, "9786024246945", "Available" },
                    { 6, "9786231864482", "Pending" },
                    { 7, "9786231864482", "Available" },
                    { 8, "9786231864482", "Available" },
                    { 9, "9786231864482", "Available" },
                    { 10, "9786231864482", "Available" },
                    { 11, "9786024410209", "Pending" },
                    { 12, "9786024410209", "Borrowed" },
                    { 13, "9786024410209", "Borrowed" },
                    { 14, "9786024410209", "Available" },
                    { 15, "9786024410209", "Available" }
                });

            migrationBuilder.InsertData(
                table: "BookGenres",
                columns: new[] { "bookId", "genreId" },
                values: new object[,]
                {
                    { "9786024246945", 1 },
                    { "9786024246945", 2 },
                    { "9786024410209", 1 },
                    { "9786024410209", 3 },
                    { "9786231864482", 1 },
                    { "9786231864482", 2 }
                });

            migrationBuilder.InsertData(
                table: "BooksPublished",
                columns: new[] { "bookPublishedId", "bookId", "publisherId" },
                values: new object[,]
                {
                    { 1, "9786024246945", 1 },
                    { 2, "9786231864482", 2 },
                    { 3, "9786024410209", 3 }
                });

            migrationBuilder.InsertData(
                table: "Borrows",
                columns: new[] { "borrowId", "borrowDate", "duration", "returnDate", "status", "userId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2025, 1, 17), 7, new DateOnly(2025, 1, 24), "Approved", "337504440000002" },
                    { 2, null, 3, null, "Pending", "447502220000003" },
                    { 3, null, 5, null, "Approved", "337502220000004" },
                    { 4, new DateOnly(2025, 1, 4), 7, new DateOnly(2025, 1, 11), "Approved", "337504440000002" }
                });

            migrationBuilder.InsertData(
                table: "CartDetails",
                columns: new[] { "bookId", "cartId" },
                values: new object[] { "9786231864482", 1 });

            migrationBuilder.InsertData(
                table: "BorrowedBooks",
                columns: new[] { "borrowId", "copyId", "returnDate" },
                values: new object[,]
                {
                    { 1, 1, null },
                    { 2, 2, null },
                    { 4, 3, new DateTime(2025, 1, 10, 15, 30, 23, 0, DateTimeKind.Unspecified) },
                    { 2, 6, null },
                    { 2, 11, null },
                    { 1, 12, null },
                    { 3, 13, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_userId",
                table: "Accounts",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Authorships_authorId",
                table: "Authorships",
                column: "authorId");

            migrationBuilder.CreateIndex(
                name: "IX_Authorships_bookId",
                table: "Authorships",
                column: "bookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCopies_bookId",
                table: "BookCopies",
                column: "bookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_genreId",
                table: "BookGenres",
                column: "genreId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksPublished_bookId",
                table: "BooksPublished",
                column: "bookId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksPublished_publisherId",
                table: "BooksPublished",
                column: "publisherId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_borrowId",
                table: "BorrowedBooks",
                column: "borrowId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_userId",
                table: "Borrows",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_bookId",
                table: "CartDetails",
                column: "bookId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_userId",
                table: "Sessions",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_userEmail",
                table: "Users",
                column: "userEmail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Authorships");

            migrationBuilder.DropTable(
                name: "BookGenres");

            migrationBuilder.DropTable(
                name: "BooksPublished");

            migrationBuilder.DropTable(
                name: "BorrowedBooks");

            migrationBuilder.DropTable(
                name: "CartDetails");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "BookCopies");

            migrationBuilder.DropTable(
                name: "Borrows");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
