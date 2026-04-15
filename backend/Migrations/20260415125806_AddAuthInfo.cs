using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventBookingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    event_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    location = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    image_url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    tag = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    password_hash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    event_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    booking_time = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.id);
                    table.ForeignKey(
                        name: "FK_bookings_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bookings_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "events",
                columns: new[] { "id", "capacity", "description", "event_date", "image_url", "location", "price", "tag", "title" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), 50, "Join the world's most innovative minds for three days of immersive exploration into the future of technology.", new DateTime(2024, 10, 24, 19, 30, 0, 0, DateTimeKind.Unspecified), "https://lh3.googleusercontent.com/aida-public/AB6AXuCvGkENIWPDy4TRzlnkgM424Lvmip--1JIIakXEublw3QtgVZUO7-hliKlQ87ZIx_d54BUEua_2fMBOxgNgXaxse14e399RKb1Xc8WdTeqhThGrrBcKAOoJExFcv6Xdvaipp8AmK4U0eNV_oW1KdAZOgxykr7MykTRDeMnu7DxTFcwY56uGRMV3VAM4jMgPsMWYDizjakJYXmNMpLLrpVPwG_gDwePv5rC90ATocgvISE_RLSeaVoolVGSGCeb5z0J2n-i7PUjx7Kg", "Monaco Yacht Club", 2400m, "Urgent", "The Celestial Soirée" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 100, "Immersive exploration into the future of technology and quantum computing breakthroughs.", new DateTime(2024, 11, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), "https://lh3.googleusercontent.com/aida-public/AB6AXuBmbRv6GuTGGjKh3p0AhtTfYWSftNgc-Gc-Tu_RhFdpSsacx-SiFWpacdeU_XNkItBROVt-maUEbGwxJpe4-nDMFD_gs8zY6_ycn81qV6oNELMaTxnfSLt7xQ2tbcbLD2WvFdKDu5Ryn1hV-cmWwyl_doWoH_-9QBnybx8B1BXuL20WAzFmevr4zfZLktOjW6xylKHpdeyJeYr6YPzlSlvB5tUXZuO0povkrONtGtmhlwfRWtl8lhf2DqnT2avC4NUBvL4VzzdAhUI", "The Ritz-Carlton, Tokyo", 850m, "Summit", "Quantum Nexus 2024" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), 200, "Exclusive art exhibition showcasing minimalist contemporary sculptures on pedestals.", new DateTime(2024, 12, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), "https://lh3.googleusercontent.com/aida-public/AB6AXuCSEcqyhxN2gOg-s3dPxgVYxSAJEGGvKoES2x_SBdH0cltn8Si5oRN9Y3x3lmhR7qMoVKdHYdnIKsvRLxnZz3mQyuWQHUQp3Yi9zst6J09mMAzuuMyQ6UIULg3tSUo9ntOpmuONYzs2XYrrMO-v0e6hjZHLjtVzrGkY_crfYof1Eke6wn2DYGa5uKnW91Hiq0VZDDvXXNTMgnr978T4Vc8rD4FfYi_186ae-00gvAYVSfusySKDKxtlzBY5o4j2gtFzHd2OHnb4AsA", "Gagosian Gallery, NYC", 120m, "Exhibition", "Visions of the Void" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), 30, "Sunset over rolling vineyard hills with rows of grapevines and a rustic stone villa.", new DateTime(2024, 10, 18, 15, 0, 0, 0, DateTimeKind.Unspecified), "https://lh3.googleusercontent.com/aida-public/AB6AXuBNdZY1P57wf2iuWDV9TGD9Kkat74q3_IEvyN8mTi4CoGj7jZkPsADtB7I_NiTW3EQBcqBnh49M0ZVR_qs0f3qu8KgbjKmfIS97l6iXZBBqNXGjJfejQU5UQ0x4CxXf7GQtkytQoVIqRpHuc_HHxG5uPaBtEXsC85TAxHn_BPMghMFjOWlDy2ToAelyy7oI3MbKM7XYbBgGJ2rKbLUSkbWiOi1KD74xaSkDy_BUT8mq2IhcoBvB-0CYhU3sp8-9w7KQmv6JGG9qlUw", "Castello di Ama, Tuscany", 3750m, "Exclusive Retreat", "Tuscan Harvest Weekend" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), 80, "Intimate jazz club setting with warm lighting, a grand piano, and soft silhouettes of performers.", new DateTime(2024, 11, 2, 21, 0, 0, 0, DateTimeKind.Unspecified), "https://lh3.googleusercontent.com/aida-public/AB6AXuCnrtxndFw8m4ZqjOg1oEYrh1mYCDguAqGXm-e-_obeTiZFMFvknBNDIhNei5HahyGchwH-H9nznKslm_ibWChi0we8ZnffbW3yw1II5MzcTpSaR1BcDauD0N9TciipyoTz_ljeMeRLG8pCEh1BcGZ271BvnYPhkd6UdCfU4VPOOu0Ptvf3Aw5PQ9NdA6eR0d1LQtuaHRPzAgP2f1CahHeGwDXb9qC7RQ9isitKnRn80FB7WQYKq4od5jLnWTh86qLDW-Rrw1mhrqc", "Saint-Germain-des-Prés, Paris", 185m, "Live Performance", "Blue Note Sessions" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), 500, "Focuses on how integrated systems create seamless experiences for end-users across global infrastructures.", new DateTime(2024, 10, 24, 8, 0, 0, 0, DateTimeKind.Unspecified), "https://lh3.googleusercontent.com/aida-public/AB6AXuDRveXypyiMbHbaEnHAvtn41EcT6GJ68rsRClmQQkRIf8LzI7iHTzzXX67vwvnyWBviNfeY9i5TwEPp90IYmYW_Q2L5x-bGuA-6BomqjqLwul-rTXx-kSLa0rzhVElr0U-p3hZWc1N-ZZFagXsUTk_oVvSEf_aOXtzEELoKH84at80F4_ATvi923bS_E4CevM6DoicY_ZJx2tVYgi8HGGIzZ7fQ0MJ88okUivjrjJ1lSrqNQaHUIHuMb9dc79NtqFKcKegR4AyEfDQ", "Grand Marina Pavilion, SF", 850m, "Technology", "Global Tech Summit 2024" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), 1000, "A grand architectural view of a modern symphony hall with warm wooden panels.", new DateTime(2024, 10, 24, 19, 30, 0, 0, DateTimeKind.Unspecified), "https://lh3.googleusercontent.com/aida-public/AB6AXuDuq4takxdmRKsf5Jqu87OMUCSeb4zHMBgS5WReSut7Fqbt3jZmpZPtUl9TTIXEeLXpbT-Xfmt9Lc57trLeOcGyR-Zx2LCeXjB8HOImcWz61njFh1_8wlvx_eZDyrX9mRRDub8URlyXnyTN4ARsn7INFmfmfKvNpewN1QQqkL6KEbMnibkfF-QbfAEOQpFhVw8Q-9dWclb6WKvnr1Rlfn_LbH_8PcjSjL8xRThNknrpJZvjwOMuSTss79qqG95Ir4CdqIabsR5SWPo", "The Glass Pavilion, London", 120m, "Gala", "Annual Symphony Gala" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), 150, "Transforming supply chain ecosystems using bleeding edge automated sorting and AI-driven node logistics.", new DateTime(2024, 12, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1579621970588-a35d0e7ab9b6?auto=format&fit=crop&q=80", "Messe Frankfurt", 350m, "Seminar", "Future of Logistics Seminar" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), 20, "A 10-course experiential dining pop-up exploring the nexus of gastronomy and molecular science.", new DateTime(2024, 11, 20, 19, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1559339352-11d035aa65de?auto=format&fit=crop&q=80", "Noma Test Kitchen, CPH", 1450m, "Pop-Up", "Culinary Avant-Garde" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), 40, "Intimate weekend retreat focusing on narrative design, rapid prototyping, and self-publishing pipelines.", new DateTime(2025, 1, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1550745165-9bc0b252726f?auto=format&fit=crop&q=80", "Lake Tahoe Cabin Resort", 450m, "Retreat", "Indie Game Developers Retreat" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookings_event_id",
                table: "bookings",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_user_id_event_id",
                table: "bookings",
                columns: new[] { "user_id", "event_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
