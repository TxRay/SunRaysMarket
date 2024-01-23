using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        Street = table.Column<string>(
                            type: "character varying(2048)",
                            maxLength: 2048,
                            nullable: false
                        ),
                        City = table.Column<string>(
                            type: "character varying(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        State = table.Column<string>(
                            type: "character varying(5)",
                            maxLength: 5,
                            nullable: false
                        ),
                        PostalCode = table.Column<string>(
                            type: "character varying(10)",
                            maxLength: 10,
                            nullable: false
                        ),
                        Country = table.Column<string>(
                            type: "character varying(30)",
                            maxLength: 30,
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        CustomerId = table.Column<int>(type: "integer", nullable: false),
                        CreatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        ),
                        UpdatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        Name = table.Column<string>(
                            type: "character varying(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        Slug = table.Column<string>(
                            type: "character varying(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        Description = table.Column<string>(
                            type: "text",
                            maxLength: 4000,
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        Name = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: true
                        ),
                        NormalizedName = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: true
                        ),
                        ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "TimeSlotDefinitions",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        StartTimeMinutes = table.Column<int>(type: "integer", nullable: false),
                        EndTimeMinutes = table.Column<int>(type: "integer", nullable: false),
                        OrderType = table.Column<int>(type: "integer", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlotDefinitions", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "UnitsOfMeasure",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        Name = table.Column<string>(
                            type: "character varying(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        Abbreviation = table.Column<string>(
                            type: "character varying(10)",
                            maxLength: 10,
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitsOfMeasure", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table =>
                    new
                    {
                        Id = table.Column<int>(type: "integer", nullable: false),
                        LocationName = table.Column<string>(
                            type: "character varying(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        PhoneNumber = table.Column<string>(
                            type: "character varying(15)",
                            maxLength: 15,
                            nullable: false
                        ),
                        EmailAddress = table.Column<string>(
                            type: "character varying(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        ManagerName = table.Column<string>(
                            type: "character varying(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        CreatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        ),
                        UpdatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_Address_Id",
                        column: x => x.Id,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        FirstName = table.Column<string>(type: "text", nullable: false),
                        LastName = table.Column<string>(type: "text", nullable: false),
                        AddressId = table.Column<int>(type: "integer", nullable: true),
                        UserName = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: true
                        ),
                        NormalizedUserName = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: true
                        ),
                        Email = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: true
                        ),
                        NormalizedEmail = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: true
                        ),
                        EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                        PasswordHash = table.Column<string>(type: "text", nullable: true),
                        SecurityStamp = table.Column<string>(type: "text", nullable: true),
                        ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                        PhoneNumber = table.Column<string>(type: "text", nullable: true),
                        PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                        TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                        LockoutEnd = table.Column<DateTimeOffset>(
                            type: "timestamp with time zone",
                            nullable: true
                        ),
                        LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                        AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Lists",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        Title = table.Column<string>(
                            type: "character varying(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        Slug = table.Column<string>(
                            type: "character varying(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        Description = table.Column<string>(type: "text", nullable: false),
                        DepartmentId = table.Column<int>(type: "integer", nullable: true),
                        CreatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        ),
                        UpdatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lists_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        DepartmentId = table.Column<int>(type: "integer", nullable: false),
                        Name = table.Column<string>(
                            type: "character varying(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        Slug = table.Column<string>(
                            type: "character varying(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        CreatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        ),
                        UpdatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTypes_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        RoleId = table.Column<int>(type: "integer", nullable: false),
                        ClaimType = table.Column<string>(type: "text", nullable: true),
                        ClaimValue = table.Column<string>(type: "text", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        StoreId = table.Column<int>(type: "integer", nullable: false),
                        TimeSlotDefinitionId = table.Column<int>(type: "integer", nullable: false),
                        Date = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        Capacity = table.Column<int>(type: "integer", nullable: false),
                        Filled = table.Column<int>(
                            type: "integer",
                            nullable: false,
                            defaultValue: 0
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSlots_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_TimeSlots_TimeSlotDefinitions_TimeSlotDefinitionId",
                        column: x => x.TimeSlotDefinitionId,
                        principalTable: "TimeSlotDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        UserId = table.Column<int>(type: "integer", nullable: false),
                        CartId = table.Column<int>(type: "integer", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        UserId = table.Column<int>(type: "integer", nullable: false),
                        ClaimType = table.Column<string>(type: "text", nullable: true),
                        ClaimValue = table.Column<string>(type: "text", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table =>
                    new
                    {
                        LoginProvider = table.Column<string>(type: "text", nullable: false),
                        ProviderKey = table.Column<string>(type: "text", nullable: false),
                        ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                        UserId = table.Column<int>(type: "integer", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table =>
                    new
                    {
                        UserId = table.Column<int>(type: "integer", nullable: false),
                        RoleId = table.Column<int>(type: "integer", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table =>
                    new
                    {
                        UserId = table.Column<int>(type: "integer", nullable: false),
                        LoginProvider = table.Column<string>(type: "text", nullable: false),
                        Name = table.Column<string>(type: "text", nullable: false),
                        Value = table.Column<string>(type: "text", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_UserTokens",
                        x =>
                            new
                            {
                                x.UserId,
                                x.LoginProvider,
                                x.Name
                            }
                    );
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        Name = table.Column<string>(
                            type: "character varying(100)",
                            maxLength: 100,
                            nullable: false
                        ),
                        Slug = table.Column<string>(
                            type: "character varying(100)",
                            maxLength: 100,
                            nullable: false
                        ),
                        Description = table.Column<string>(type: "text", nullable: false),
                        PhotoUrl = table.Column<string>(
                            type: "character varying(255)",
                            maxLength: 255,
                            nullable: false
                        ),
                        Price = table.Column<float>(type: "numeric(5,2)", nullable: false),
                        DiscountPercent = table.Column<float>(
                            type: "numeric(3,2)",
                            nullable: false
                        ),
                        ProductTypeId = table.Column<int>(type: "integer", nullable: false),
                        Measure = table.Column<float>(type: "numeric(5,2)", nullable: false),
                        UnitOfMeasureId = table.Column<int>(type: "integer", nullable: false),
                        CreatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        ),
                        UpdatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Product_UnitsOfMeasure_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "UnitsOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        CustomerId = table.Column<int>(type: "integer", nullable: false),
                        StoreId = table.Column<int>(type: "integer", nullable: false),
                        TimeSlotId = table.Column<int>(type: "integer", nullable: false),
                        OrderType = table.Column<int>(type: "integer", nullable: false),
                        Subtotal = table.Column<float>(
                            type: "real",
                            nullable: false,
                            defaultValue: 0f
                        ),
                        Discount = table.Column<float>(
                            type: "real",
                            nullable: false,
                            defaultValue: 0f
                        ),
                        Tax = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                        Total = table.Column<float>(
                            type: "real",
                            nullable: false,
                            defaultValue: 0f
                        ),
                        Status = table.Column<int>(
                            type: "integer",
                            nullable: false,
                            defaultValue: 0
                        ),
                        CreatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        ),
                        UpdatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Orders_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Orders_TimeSlots_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        CartId = table.Column<int>(type: "integer", nullable: false),
                        ProductId = table.Column<int>(type: "integer", nullable: false),
                        Quantity = table.Column<int>(type: "integer", nullable: false),
                        CreatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        ),
                        UpdatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_CartItems_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ListProduct",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        ListId = table.Column<int>(type: "integer", nullable: false),
                        ProductId = table.Column<int>(type: "integer", nullable: false),
                        ListsId = table.Column<int>(type: "integer", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListProduct_Lists_ListId",
                        column: x => x.ListId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                    table.ForeignKey(
                        name: "FK_ListProduct_Lists_ListsId",
                        column: x => x.ListsId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_ListProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ProductInventory",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        ProductId = table.Column<int>(type: "integer", nullable: false),
                        StoreId = table.Column<int>(type: "integer", nullable: false),
                        Sku = table.Column<string>(
                            type: "character varying(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        Barcode = table.Column<string>(
                            type: "character varying(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        Plu = table.Column<string>(
                            type: "character varying(50)",
                            maxLength: 50,
                            nullable: true
                        ),
                        Quantity = table.Column<int>(type: "integer", nullable: false),
                        CreatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        ),
                        UpdatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInventory_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_ProductInventory_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "OrderLine",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        OrderId = table.Column<int>(type: "integer", nullable: false),
                        ItemId = table.Column<int>(type: "integer", nullable: false),
                        OrderSubstitutionId = table.Column<int>(type: "integer", nullable: true),
                        UnitOfMeasurement = table.Column<string>(type: "text", nullable: false),
                        Quantity = table.Column<int>(type: "integer", nullable: false),
                        Price = table.Column<float>(type: "real", nullable: false),
                        Discount = table.Column<float>(type: "real", nullable: false),
                        TotalPrice = table.Column<float>(type: "real", nullable: false),
                        CreatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        ),
                        UpdatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLine_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_OrderLine_Product_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        Code = table.Column<int>(type: "integer", nullable: false),
                        CustomerId = table.Column<int>(type: "integer", nullable: false),
                        OrderId = table.Column<int>(type: "integer", nullable: false),
                        Status = table.Column<int>(type: "integer", nullable: false),
                        PaymentMethod = table.Column<string>(type: "text", nullable: false),
                        AmountPaid = table.Column<float>(type: "real", nullable: false),
                        TimeSlotId = table.Column<int>(type: "integer", nullable: true),
                        CreatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        ),
                        UpdatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Transactions_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Transactions_TimeSlots_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "OrderSubstitution",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        OrderLineId = table.Column<int>(type: "integer", nullable: false),
                        OriginalItemId = table.Column<int>(type: "integer", nullable: false),
                        SubstituteItemId = table.Column<int>(type: "integer", nullable: false),
                        OrderId = table.Column<int>(type: "integer", nullable: true),
                        CreatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        ),
                        UpdatedAt = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: true,
                            defaultValueSql: "CURRENT_TIMESTAMP"
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSubstitution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderSubstitution_OrderLine_OrderLineId",
                        column: x => x.OrderLineId,
                        principalTable: "OrderLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                    table.ForeignKey(
                        name: "FK_OrderSubstitution_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_OrderSubstitution_Product_OriginalItemId",
                        column: x => x.OriginalItemId,
                        principalTable: "Product",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_OrderSubstitution_Product_SubstituteItemId",
                        column: x => x.SubstituteItemId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerId",
                table: "Carts",
                column: "CustomerId",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CartId",
                table: "Customers",
                column: "CartId",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name",
                table: "Departments",
                column: "Name",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Slug",
                table: "Departments",
                column: "Slug",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_ListProduct_ListId_ProductId",
                table: "ListProduct",
                columns: new[] { "ListId", "ProductId" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_ListProduct_ListsId",
                table: "ListProduct",
                column: "ListsId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ListProduct_ProductId",
                table: "ListProduct",
                column: "ProductId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Lists_DepartmentId",
                table: "Lists",
                column: "DepartmentId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Lists_Slug",
                table: "Lists",
                column: "Slug",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Lists_Title",
                table: "Lists",
                column: "Title",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_ItemId",
                table: "OrderLine",
                column: "ItemId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_OrderId_ItemId",
                table: "OrderLine",
                columns: new[] { "OrderId", "ItemId" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId_StoreId_TimeSlotId_OrderType",
                table: "Orders",
                columns: new[] { "CustomerId", "StoreId", "TimeSlotId", "OrderType" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StoreId",
                table: "Orders",
                column: "StoreId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TimeSlotId",
                table: "Orders",
                column: "TimeSlotId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderSubstitution_OrderId",
                table: "OrderSubstitution",
                column: "OrderId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderSubstitution_OrderLineId",
                table: "OrderSubstitution",
                column: "OrderLineId",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderSubstitution_OriginalItemId",
                table: "OrderSubstitution",
                column: "OriginalItemId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_OrderSubstitution_SubstituteItemId",
                table: "OrderSubstitution",
                column: "SubstituteItemId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product",
                column: "ProductTypeId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitOfMeasureId",
                table: "Product",
                column: "UnitOfMeasureId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventory_ProductId_StoreId",
                table: "ProductInventory",
                columns: new[] { "ProductId", "StoreId" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventory_StoreId",
                table: "ProductInventory",
                column: "StoreId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_DepartmentId",
                table: "ProductTypes",
                column: "DepartmentId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_Name",
                table: "ProductTypes",
                column: "Name",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_Slug",
                table: "ProductTypes",
                column: "Slug",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId"
            );

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlotDefinitions_StartTimeMinutes_EndTimeMinutes",
                table: "TimeSlotDefinitions",
                columns: new[] { "StartTimeMinutes", "EndTimeMinutes" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_StoreId_TimeSlotDefinitionId_Date",
                table: "TimeSlots",
                columns: new[] { "StoreId", "TimeSlotDefinitionId", "Date" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_TimeSlotDefinitionId",
                table: "TimeSlots",
                column: "TimeSlotDefinitionId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions",
                column: "CustomerId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OrderId",
                table: "Transactions",
                column: "OrderId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TimeSlotId",
                table: "Transactions",
                column: "TimeSlotId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_UnitsOfMeasure_Abbreviation",
                table: "UnitsOfMeasure",
                column: "Abbreviation",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_UnitsOfMeasure_Name",
                table: "UnitsOfMeasure",
                column: "Name",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId"
            );

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                column: "AddressId"
            );

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "CartItems");

            migrationBuilder.DropTable(name: "ListProduct");

            migrationBuilder.DropTable(name: "OrderSubstitution");

            migrationBuilder.DropTable(name: "ProductInventory");

            migrationBuilder.DropTable(name: "RoleClaims");

            migrationBuilder.DropTable(name: "Transactions");

            migrationBuilder.DropTable(name: "UserClaims");

            migrationBuilder.DropTable(name: "UserLogins");

            migrationBuilder.DropTable(name: "UserRoles");

            migrationBuilder.DropTable(name: "UserTokens");

            migrationBuilder.DropTable(name: "Lists");

            migrationBuilder.DropTable(name: "OrderLine");

            migrationBuilder.DropTable(name: "Roles");

            migrationBuilder.DropTable(name: "Orders");

            migrationBuilder.DropTable(name: "Product");

            migrationBuilder.DropTable(name: "Customers");

            migrationBuilder.DropTable(name: "TimeSlots");

            migrationBuilder.DropTable(name: "ProductTypes");

            migrationBuilder.DropTable(name: "UnitsOfMeasure");

            migrationBuilder.DropTable(name: "Carts");

            migrationBuilder.DropTable(name: "Users");

            migrationBuilder.DropTable(name: "Stores");

            migrationBuilder.DropTable(name: "TimeSlotDefinitions");

            migrationBuilder.DropTable(name: "Departments");

            migrationBuilder.DropTable(name: "Address");
        }
    }
}
