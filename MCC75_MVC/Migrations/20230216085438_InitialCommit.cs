﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCC75_MVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_employess",
                columns: table => new
                {
                    nik = table.Column<string>(type: "nchar(5)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    hiring_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employess", x => x.nik);
                    table.UniqueConstraint("AK_tb_m_employess_email_phone_number", x => new { x.email, x.phone_number });
                });

            migrationBuilder.CreateTable(
                name: "tb_m_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_universities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_universities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_accounts",
                columns: table => new
                {
                    employee_nik = table.Column<string>(type: "nchar(5)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_accounts", x => x.employee_nik);
                    table.ForeignKey(
                        name: "FK_tb_m_accounts_tb_m_employess_employee_nik",
                        column: x => x.employee_nik,
                        principalTable: "tb_m_employess",
                        principalColumn: "nik",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_educations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    major = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    degree = table.Column<string>(type: "nchar(2)", nullable: false),
                    gpa = table.Column<float>(type: "real", nullable: false),
                    university_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_educations", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_m_educations_tb_m_universities_university_id",
                        column: x => x.university_id,
                        principalTable: "tb_m_universities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_account_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account_nik = table.Column<string>(type: "nchar(5)", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_account_roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_tr_account_roles_tb_m_accounts_account_nik",
                        column: x => x.account_nik,
                        principalTable: "tb_m_accounts",
                        principalColumn: "employee_nik",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_tr_account_roles_tb_m_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "tb_m_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_profillings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employee_nik = table.Column<string>(type: "nchar(5)", nullable: false),
                    employee_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_profillings", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_tr_profillings_tb_m_educations_employee_id",
                        column: x => x.employee_id,
                        principalTable: "tb_m_educations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_tr_profillings_tb_m_employess_employee_nik",
                        column: x => x.employee_nik,
                        principalTable: "tb_m_employess",
                        principalColumn: "nik",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_educations_university_id",
                table: "tb_m_educations",
                column: "university_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_account_roles_account_nik",
                table: "tb_tr_account_roles",
                column: "account_nik");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_account_roles_role_id",
                table: "tb_tr_account_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_profillings_employee_id",
                table: "tb_tr_profillings",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_profillings_employee_nik",
                table: "tb_tr_profillings",
                column: "employee_nik");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_tr_account_roles");

            migrationBuilder.DropTable(
                name: "tb_tr_profillings");

            migrationBuilder.DropTable(
                name: "tb_m_accounts");

            migrationBuilder.DropTable(
                name: "tb_m_roles");

            migrationBuilder.DropTable(
                name: "tb_m_educations");

            migrationBuilder.DropTable(
                name: "tb_m_employess");

            migrationBuilder.DropTable(
                name: "tb_m_universities");
        }
    }
}
