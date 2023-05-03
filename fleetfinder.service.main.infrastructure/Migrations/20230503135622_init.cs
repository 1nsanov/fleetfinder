using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using fleetfinder.service.main.domain.Enums.Common;
using fleetfinder.service.main.domain.Enums.Order.Cargo;
using fleetfinder.service.main.domain.Enums.Transport;
using fleetfinder.service.main.domain.Enums.Transport.Cargo;
using fleetfinder.service.main.domain.Enums.Transport.Passenger;
using fleetfinder.service.main.domain.Enums.Transport.Special;

#nullable disable

namespace fleetfinder.service.main.infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:cargo_body_kind", "auto_train,beam_vehicle,flatbed_vehicle,hydraulic_lift,cargo_passenger,oversized_vehicle,isothermal_vehicle,container_vehicle,feed_vehicle,flour_vehicle,open_vehicle,panel_vehicle,pickup,pyramid,semi_trailer,light_trailer,refrigerator,roll_vehicle,tractor_unit,agricultural_grain_vehicle,livestock_vehicle,timber_vehicle,glass_vehicle,coupling,tank_container,curtain_sider,insulated_vehicle,pipe_vehicle,trailer_truck,van,solid_metal,chip_car")
                .Annotation("Npgsql:Enum:cargo_load_type", "top,rear,side,with_hydroboard,with_full_liftgate,with_ramps_or_discharges,with_tarmac,with_stakes,with_air_suspension")
                .Annotation("Npgsql:Enum:cargo_loaders", "without_movers,one_movers,two_movers,three_movers")
                .Annotation("Npgsql:Enum:cargo_transportation_kind", "cargo_taxi,apartment_moving,office_moving,furniture_transport,food_transport,intercity_transport,international_transport,lcl_transport,livestock_transport,personal_items_transport,appliance_transport,fruit_transport,vegetable_transport,country_house_moving,warehouse_moving,piano_transport,safe_transport,refrigerator_transport,equipment_transport,construction_materials_transport,motorcycle_transport")
                .Annotation("Npgsql:Enum:cargo_transportation_type", "full_load,partial_load,overload")
                .Annotation("Npgsql:Enum:cargo_type", "t1,t2,t3,t5,t10,t20")
                .Annotation("Npgsql:Enum:experience_work", "less_year1,year1,year2,year3,year4,year5,year6,year7,year8,year9,more_year10")
                .Annotation("Npgsql:Enum:passenger_facilities", "comfortable,economy,standard")
                .Annotation("Npgsql:Enum:passenger_option", "audio_system_and_microphone,air_conditioner,ventilation_system,large_luggage_compartments,monitor_and_dvd,individual_lighting,seat_heating,spacious_salon,safety_belts,climate_control,toilet,sleeping_places")
                .Annotation("Npgsql:Enum:passenger_rental_duration", "per_day,one_day,per_hour,per_month,long_term")
                .Annotation("Npgsql:Enum:passenger_transportation_kind", "order,children,tourists,intercity,abroad_trips,corporate,airport_transfer,employee_delivery,wedding,vip,medical,funeral,pet,car_home,excursion,party_bus,courier")
                .Annotation("Npgsql:Enum:passenger_type", "taxi,limousine,minivan,bus,shiftw,water")
                .Annotation("Npgsql:Enum:payment_method", "cash,non_cash,cash_and_non_cash,card_payment")
                .Annotation("Npgsql:Enum:payment_order", "prepayment,payment_upon_delivery,installment_payment")
                .Annotation("Npgsql:Enum:region", "bender,tiraspol,grigoriopol,dubasari,camenka,ribnitsa,slobozia")
                .Annotation("Npgsql:Enum:special_type", "excavator,aerial_platform,truck_crane,truck_fuel,bulldozer,hydra_hammer,grader,crag_loader,road_roller,mini_loader,mini_excavator,waste_car,dump_truck,tractor,front_loader,truck_cement,excavator_loader,yamobur")
                .Annotation("Npgsql:Enum:state", "actual,archived");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FullName_First = table.Column<string>(type: "text", nullable: false),
                    FullName_Second = table.Column<string>(type: "text", nullable: false),
                    FullName_Surname = table.Column<string>(type: "text", nullable: true),
                    Organization = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Contact_PhoneViber = table.Column<string>(type: "text", nullable: true),
                    Contact_PhoneTelegram = table.Column<string>(type: "text", nullable: true),
                    Contact_PhoneWhatsapp = table.Column<string>(type: "text", nullable: true),
                    Contact_WorkingMode = table.Column<string>(type: "text", nullable: true),
                    RefreshToken_Value = table.Column<string>(type: "text", nullable: true),
                    RefreshToken_ExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CargoOrder",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<CargoType>(type: "cargo_type", nullable: false),
                    ShipmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Loaders = table.Column<CargoLoaders>(type: "cargo_loaders", nullable: true),
                    BodyKind = table.Column<CargoBodyKind>(type: "cargo_body_kind", nullable: true),
                    TransportationType = table.Column<CargoTransportationType>(type: "cargo_transportation_type", nullable: true),
                    LoadType = table.Column<CargoLoadType>(type: "cargo_load_type", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    Title = table.Column<string>(type: "text", nullable: false),
                    PickupRegion = table.Column<Region>(type: "region", nullable: false),
                    DeliverRegion = table.Column<Region>(type: "region", nullable: false),
                    MaxBudget = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentMethod = table.Column<PaymentMethod>(type: "payment_method", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoOrder_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CargoTransport",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<CargoType>(type: "cargo_type", nullable: false),
                    Body_LoadCapacity = table.Column<decimal>(type: "numeric", nullable: true),
                    Body_Length = table.Column<decimal>(type: "numeric", nullable: true),
                    Body_Width = table.Column<decimal>(type: "numeric", nullable: true),
                    Body_Height = table.Column<decimal>(type: "numeric", nullable: true),
                    Body_Volume = table.Column<decimal>(type: "numeric", nullable: true),
                    Body_Kind = table.Column<CargoBodyKind>(type: "cargo_body_kind", nullable: true),
                    TransportationKind = table.Column<CargoTransportationKind>(type: "cargo_transportation_kind", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<Region>(type: "region", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: true),
                    YearIssue = table.Column<DateOnly>(type: "date", nullable: true),
                    ExperienceWork = table.Column<ExperienceWork>(type: "experience_work", nullable: true),
                    PaymentMethod = table.Column<PaymentMethod>(type: "payment_method", nullable: true),
                    PaymentOrder = table.Column<PaymentOrder>(type: "payment_order", nullable: true),
                    Price_PerHour = table.Column<decimal>(type: "numeric", nullable: true),
                    Price_PerShift = table.Column<decimal>(type: "numeric", nullable: true),
                    Price_PerKm = table.Column<decimal>(type: "numeric", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoTransport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoTransport_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PassengerOrder",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<PassengerType>(type: "passenger_type", nullable: false),
                    RentalDuration = table.Column<PassengerRentalDuration>(type: "passenger_rental_duration", nullable: true),
                    Facilities = table.Column<PassengerFacilities>(type: "passenger_facilities", nullable: true),
                    CountSeats = table.Column<int>(type: "integer", nullable: true),
                    Option = table.Column<PassengerOption>(type: "passenger_option", nullable: true),
                    TransportationKind = table.Column<PassengerTransportationKind>(type: "passenger_transportation_kind", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    Title = table.Column<string>(type: "text", nullable: false),
                    PickupRegion = table.Column<Region>(type: "region", nullable: false),
                    DeliverRegion = table.Column<Region>(type: "region", nullable: false),
                    MaxBudget = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentMethod = table.Column<PaymentMethod>(type: "payment_method", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassengerOrder_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PassengerTransport",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<PassengerType>(type: "passenger_type", nullable: false),
                    RentalDuration = table.Column<PassengerRentalDuration>(type: "passenger_rental_duration", nullable: true),
                    Facilities = table.Column<PassengerFacilities>(type: "passenger_facilities", nullable: true),
                    CountSeats = table.Column<int>(type: "integer", nullable: true),
                    Size_Length = table.Column<decimal>(type: "numeric", nullable: true),
                    Size_Width = table.Column<decimal>(type: "numeric", nullable: true),
                    Size_Height = table.Column<decimal>(type: "numeric", nullable: true),
                    Option = table.Column<PassengerOption>(type: "passenger_option", nullable: true),
                    TransportationKind = table.Column<PassengerTransportationKind>(type: "passenger_transportation_kind", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    MinOrderTime = table.Column<decimal>(type: "numeric", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<Region>(type: "region", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: true),
                    YearIssue = table.Column<DateOnly>(type: "date", nullable: true),
                    ExperienceWork = table.Column<ExperienceWork>(type: "experience_work", nullable: true),
                    PaymentMethod = table.Column<PaymentMethod>(type: "payment_method", nullable: true),
                    PaymentOrder = table.Column<PaymentOrder>(type: "payment_order", nullable: true),
                    Price_PerHour = table.Column<decimal>(type: "numeric", nullable: true),
                    Price_PerShift = table.Column<decimal>(type: "numeric", nullable: true),
                    Price_PerKm = table.Column<decimal>(type: "numeric", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerTransport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassengerTransport_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialOrder",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<SpecialType>(type: "special_type", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    Title = table.Column<string>(type: "text", nullable: false),
                    PickupRegion = table.Column<Region>(type: "region", nullable: false),
                    DeliverRegion = table.Column<Region>(type: "region", nullable: false),
                    MaxBudget = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentMethod = table.Column<PaymentMethod>(type: "payment_method", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialOrder_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialTransport",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<SpecialType>(type: "special_type", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<Region>(type: "region", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: true),
                    YearIssue = table.Column<DateOnly>(type: "date", nullable: true),
                    ExperienceWork = table.Column<ExperienceWork>(type: "experience_work", nullable: true),
                    PaymentMethod = table.Column<PaymentMethod>(type: "payment_method", nullable: true),
                    PaymentOrder = table.Column<PaymentOrder>(type: "payment_order", nullable: true),
                    Price_PerHour = table.Column<decimal>(type: "numeric", nullable: true),
                    Price_PerShift = table.Column<decimal>(type: "numeric", nullable: true),
                    Price_PerKm = table.Column<decimal>(type: "numeric", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialTransport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialTransport_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CargoOrderImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoOrderImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoOrderImage_CargoOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "CargoOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CargoTransportImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransportId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoTransportImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoTransportImage_CargoTransport_TransportId",
                        column: x => x.TransportId,
                        principalTable: "CargoTransport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PassengerOrderImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerOrderImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassengerOrderImage_PassengerOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "PassengerOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PassengerTransportImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransportId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerTransportImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassengerTransportImage_PassengerTransport_TransportId",
                        column: x => x.TransportId,
                        principalTable: "PassengerTransport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialOrderImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialOrderImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialOrderImage_SpecialOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "SpecialOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialTransportImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransportId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialTransportImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialTransportImage_SpecialTransport_TransportId",
                        column: x => x.TransportId,
                        principalTable: "SpecialTransport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CargoOrder_UserId",
                table: "CargoOrder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoOrderImage_OrderId",
                table: "CargoOrderImage",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoTransport_UserId",
                table: "CargoTransport",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoTransportImage_TransportId",
                table: "CargoTransportImage",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerOrder_UserId",
                table: "PassengerOrder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerOrderImage_OrderId",
                table: "PassengerOrderImage",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerTransport_UserId",
                table: "PassengerTransport",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerTransportImage_TransportId",
                table: "PassengerTransportImage",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialOrder_UserId",
                table: "SpecialOrder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialOrderImage_OrderId",
                table: "SpecialOrderImage",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialTransport_UserId",
                table: "SpecialTransport",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialTransportImage_TransportId",
                table: "SpecialTransportImage",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Login",
                table: "User",
                column: "Login");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoOrderImage");

            migrationBuilder.DropTable(
                name: "CargoTransportImage");

            migrationBuilder.DropTable(
                name: "PassengerOrderImage");

            migrationBuilder.DropTable(
                name: "PassengerTransportImage");

            migrationBuilder.DropTable(
                name: "SpecialOrderImage");

            migrationBuilder.DropTable(
                name: "SpecialTransportImage");

            migrationBuilder.DropTable(
                name: "CargoOrder");

            migrationBuilder.DropTable(
                name: "CargoTransport");

            migrationBuilder.DropTable(
                name: "PassengerOrder");

            migrationBuilder.DropTable(
                name: "PassengerTransport");

            migrationBuilder.DropTable(
                name: "SpecialOrder");

            migrationBuilder.DropTable(
                name: "SpecialTransport");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
