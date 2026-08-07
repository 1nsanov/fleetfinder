using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using fleetfinder.service.main.domain.Enums.Common;
using fleetfinder.service.main.domain.Enums.Transport;
using fleetfinder.service.main.domain.Enums.Transport.Cargo;
using fleetfinder.service.main.domain.Enums.Transport.Passenger;
using fleetfinder.service.main.domain.Enums.Transport.Special;

#nullable disable

namespace fleetfinder.service.main.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoOrderImage");

            migrationBuilder.DropTable(
                name: "PassengerOrderImage");

            migrationBuilder.DropTable(
                name: "SpecialOrderImage");

            migrationBuilder.DropTable(
                name: "CargoOrder");

            migrationBuilder.DropTable(
                name: "PassengerOrder");

            migrationBuilder.DropTable(
                name: "SpecialOrder");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:cargo_body_kind", "agricultural_grain_vehicle,auto_train,beam_vehicle,cargo_passenger,chip_car,container_vehicle,coupling,curtain_sider,feed_vehicle,flatbed_vehicle,flour_vehicle,glass_vehicle,hydraulic_lift,insulated_vehicle,isothermal_vehicle,light_trailer,livestock_vehicle,open_vehicle,oversized_vehicle,panel_vehicle,pickup,pipe_vehicle,pyramid,refrigerator,roll_vehicle,semi_trailer,solid_metal,tank_container,timber_vehicle,tractor_unit,trailer_truck,van")
                .Annotation("Npgsql:Enum:cargo_transportation_kind", "apartment_moving,appliance_transport,cargo_taxi,construction_materials_transport,country_house_moving,equipment_transport,food_transport,fruit_transport,furniture_transport,intercity_transport,international_transport,lcl_transport,livestock_transport,motorcycle_transport,office_moving,personal_items_transport,piano_transport,refrigerator_transport,safe_transport,vegetable_transport,warehouse_moving")
                .Annotation("Npgsql:Enum:cargo_type", "t1,t10,t2,t20,t3,t5")
                .Annotation("Npgsql:Enum:experience_work", "less_year1,more_year10,year1,year2,year3,year4,year5,year6,year7,year8,year9")
                .Annotation("Npgsql:Enum:passenger_facilities", "comfortable,economy,standard")
                .Annotation("Npgsql:Enum:passenger_option", "air_conditioner,audio_system_and_microphone,climate_control,individual_lighting,large_luggage_compartments,monitor_and_dvd,safety_belts,seat_heating,sleeping_places,spacious_salon,toilet,ventilation_system")
                .Annotation("Npgsql:Enum:passenger_rental_duration", "long_term,one_day,per_day,per_hour,per_month")
                .Annotation("Npgsql:Enum:passenger_transportation_kind", "abroad_trips,airport_transfer,car_home,children,corporate,courier,employee_delivery,excursion,funeral,intercity,medical,order,party_bus,pet,tourists,vip,wedding")
                .Annotation("Npgsql:Enum:passenger_type", "bus,limousine,minivan,shiftw,taxi,water")
                .Annotation("Npgsql:Enum:payment_method", "card_payment,cash,cash_and_non_cash,non_cash")
                .Annotation("Npgsql:Enum:payment_order", "installment_payment,payment_upon_delivery,prepayment")
                .Annotation("Npgsql:Enum:region", "bender,camenka,dubasari,grigoriopol,ribnitsa,slobozia,tiraspol")
                .Annotation("Npgsql:Enum:special_type", "aerial_platform,bulldozer,crag_loader,dump_truck,excavator,excavator_loader,front_loader,grader,hydra_hammer,mini_excavator,mini_loader,road_roller,tractor,truck_cement,truck_crane,truck_fuel,waste_car,yamobur")
                .Annotation("Npgsql:Enum:state", "actual,archived")
                .OldAnnotation("Npgsql:Enum:cargo_body_kind", "agricultural_grain_vehicle,auto_train,beam_vehicle,cargo_passenger,chip_car,container_vehicle,coupling,curtain_sider,feed_vehicle,flatbed_vehicle,flour_vehicle,glass_vehicle,hydraulic_lift,insulated_vehicle,isothermal_vehicle,light_trailer,livestock_vehicle,open_vehicle,oversized_vehicle,panel_vehicle,pickup,pipe_vehicle,pyramid,refrigerator,roll_vehicle,semi_trailer,solid_metal,tank_container,timber_vehicle,tractor_unit,trailer_truck,van")
                .OldAnnotation("Npgsql:Enum:cargo_load_type", "rear,side,top,with_air_suspension,with_full_liftgate,with_hydroboard,with_ramps_or_discharges,with_stakes,with_tarmac")
                .OldAnnotation("Npgsql:Enum:cargo_loaders", "one_movers,three_movers,two_movers,without_movers")
                .OldAnnotation("Npgsql:Enum:cargo_transportation_kind", "apartment_moving,appliance_transport,cargo_taxi,construction_materials_transport,country_house_moving,equipment_transport,food_transport,fruit_transport,furniture_transport,intercity_transport,international_transport,lcl_transport,livestock_transport,motorcycle_transport,office_moving,personal_items_transport,piano_transport,refrigerator_transport,safe_transport,vegetable_transport,warehouse_moving")
                .OldAnnotation("Npgsql:Enum:cargo_transportation_type", "full_load,overload,partial_load")
                .OldAnnotation("Npgsql:Enum:cargo_type", "t1,t10,t2,t20,t3,t5")
                .OldAnnotation("Npgsql:Enum:experience_work", "less_year1,more_year10,year1,year2,year3,year4,year5,year6,year7,year8,year9")
                .OldAnnotation("Npgsql:Enum:passenger_facilities", "comfortable,economy,standard")
                .OldAnnotation("Npgsql:Enum:passenger_option", "air_conditioner,audio_system_and_microphone,climate_control,individual_lighting,large_luggage_compartments,monitor_and_dvd,safety_belts,seat_heating,sleeping_places,spacious_salon,toilet,ventilation_system")
                .OldAnnotation("Npgsql:Enum:passenger_rental_duration", "long_term,one_day,per_day,per_hour,per_month")
                .OldAnnotation("Npgsql:Enum:passenger_transportation_kind", "abroad_trips,airport_transfer,car_home,children,corporate,courier,employee_delivery,excursion,funeral,intercity,medical,order,party_bus,pet,tourists,vip,wedding")
                .OldAnnotation("Npgsql:Enum:passenger_type", "bus,limousine,minivan,shiftw,taxi,water")
                .OldAnnotation("Npgsql:Enum:payment_method", "card_payment,cash,cash_and_non_cash,non_cash")
                .OldAnnotation("Npgsql:Enum:payment_order", "installment_payment,payment_upon_delivery,prepayment")
                .OldAnnotation("Npgsql:Enum:region", "bender,camenka,dubasari,grigoriopol,ribnitsa,slobozia,tiraspol")
                .OldAnnotation("Npgsql:Enum:special_type", "aerial_platform,bulldozer,crag_loader,dump_truck,excavator,excavator_loader,front_loader,grader,hydra_hammer,mini_excavator,mini_loader,road_roller,tractor,truck_cement,truck_crane,truck_fuel,waste_car,yamobur")
                .OldAnnotation("Npgsql:Enum:state", "actual,archived");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:cargo_body_kind", "agricultural_grain_vehicle,auto_train,beam_vehicle,cargo_passenger,chip_car,container_vehicle,coupling,curtain_sider,feed_vehicle,flatbed_vehicle,flour_vehicle,glass_vehicle,hydraulic_lift,insulated_vehicle,isothermal_vehicle,light_trailer,livestock_vehicle,open_vehicle,oversized_vehicle,panel_vehicle,pickup,pipe_vehicle,pyramid,refrigerator,roll_vehicle,semi_trailer,solid_metal,tank_container,timber_vehicle,tractor_unit,trailer_truck,van")
                .Annotation("Npgsql:Enum:cargo_load_type", "rear,side,top,with_air_suspension,with_full_liftgate,with_hydroboard,with_ramps_or_discharges,with_stakes,with_tarmac")
                .Annotation("Npgsql:Enum:cargo_loaders", "one_movers,three_movers,two_movers,without_movers")
                .Annotation("Npgsql:Enum:cargo_transportation_kind", "apartment_moving,appliance_transport,cargo_taxi,construction_materials_transport,country_house_moving,equipment_transport,food_transport,fruit_transport,furniture_transport,intercity_transport,international_transport,lcl_transport,livestock_transport,motorcycle_transport,office_moving,personal_items_transport,piano_transport,refrigerator_transport,safe_transport,vegetable_transport,warehouse_moving")
                .Annotation("Npgsql:Enum:cargo_transportation_type", "full_load,overload,partial_load")
                .Annotation("Npgsql:Enum:cargo_type", "t1,t10,t2,t20,t3,t5")
                .Annotation("Npgsql:Enum:experience_work", "less_year1,more_year10,year1,year2,year3,year4,year5,year6,year7,year8,year9")
                .Annotation("Npgsql:Enum:passenger_facilities", "comfortable,economy,standard")
                .Annotation("Npgsql:Enum:passenger_option", "air_conditioner,audio_system_and_microphone,climate_control,individual_lighting,large_luggage_compartments,monitor_and_dvd,safety_belts,seat_heating,sleeping_places,spacious_salon,toilet,ventilation_system")
                .Annotation("Npgsql:Enum:passenger_rental_duration", "long_term,one_day,per_day,per_hour,per_month")
                .Annotation("Npgsql:Enum:passenger_transportation_kind", "abroad_trips,airport_transfer,car_home,children,corporate,courier,employee_delivery,excursion,funeral,intercity,medical,order,party_bus,pet,tourists,vip,wedding")
                .Annotation("Npgsql:Enum:passenger_type", "bus,limousine,minivan,shiftw,taxi,water")
                .Annotation("Npgsql:Enum:payment_method", "card_payment,cash,cash_and_non_cash,non_cash")
                .Annotation("Npgsql:Enum:payment_order", "installment_payment,payment_upon_delivery,prepayment")
                .Annotation("Npgsql:Enum:region", "bender,camenka,dubasari,grigoriopol,ribnitsa,slobozia,tiraspol")
                .Annotation("Npgsql:Enum:special_type", "aerial_platform,bulldozer,crag_loader,dump_truck,excavator,excavator_loader,front_loader,grader,hydra_hammer,mini_excavator,mini_loader,road_roller,tractor,truck_cement,truck_crane,truck_fuel,waste_car,yamobur")
                .Annotation("Npgsql:Enum:state", "actual,archived")
                .OldAnnotation("Npgsql:Enum:cargo_body_kind", "agricultural_grain_vehicle,auto_train,beam_vehicle,cargo_passenger,chip_car,container_vehicle,coupling,curtain_sider,feed_vehicle,flatbed_vehicle,flour_vehicle,glass_vehicle,hydraulic_lift,insulated_vehicle,isothermal_vehicle,light_trailer,livestock_vehicle,open_vehicle,oversized_vehicle,panel_vehicle,pickup,pipe_vehicle,pyramid,refrigerator,roll_vehicle,semi_trailer,solid_metal,tank_container,timber_vehicle,tractor_unit,trailer_truck,van")
                .OldAnnotation("Npgsql:Enum:cargo_transportation_kind", "apartment_moving,appliance_transport,cargo_taxi,construction_materials_transport,country_house_moving,equipment_transport,food_transport,fruit_transport,furniture_transport,intercity_transport,international_transport,lcl_transport,livestock_transport,motorcycle_transport,office_moving,personal_items_transport,piano_transport,refrigerator_transport,safe_transport,vegetable_transport,warehouse_moving")
                .OldAnnotation("Npgsql:Enum:cargo_type", "t1,t10,t2,t20,t3,t5")
                .OldAnnotation("Npgsql:Enum:experience_work", "less_year1,more_year10,year1,year2,year3,year4,year5,year6,year7,year8,year9")
                .OldAnnotation("Npgsql:Enum:passenger_facilities", "comfortable,economy,standard")
                .OldAnnotation("Npgsql:Enum:passenger_option", "air_conditioner,audio_system_and_microphone,climate_control,individual_lighting,large_luggage_compartments,monitor_and_dvd,safety_belts,seat_heating,sleeping_places,spacious_salon,toilet,ventilation_system")
                .OldAnnotation("Npgsql:Enum:passenger_rental_duration", "long_term,one_day,per_day,per_hour,per_month")
                .OldAnnotation("Npgsql:Enum:passenger_transportation_kind", "abroad_trips,airport_transfer,car_home,children,corporate,courier,employee_delivery,excursion,funeral,intercity,medical,order,party_bus,pet,tourists,vip,wedding")
                .OldAnnotation("Npgsql:Enum:passenger_type", "bus,limousine,minivan,shiftw,taxi,water")
                .OldAnnotation("Npgsql:Enum:payment_method", "card_payment,cash,cash_and_non_cash,non_cash")
                .OldAnnotation("Npgsql:Enum:payment_order", "installment_payment,payment_upon_delivery,prepayment")
                .OldAnnotation("Npgsql:Enum:region", "bender,camenka,dubasari,grigoriopol,ribnitsa,slobozia,tiraspol")
                .OldAnnotation("Npgsql:Enum:special_type", "aerial_platform,bulldozer,crag_loader,dump_truck,excavator,excavator_loader,front_loader,grader,hydra_hammer,mini_excavator,mini_loader,road_roller,tractor,truck_cement,truck_crane,truck_fuel,waste_car,yamobur")
                .OldAnnotation("Npgsql:Enum:state", "actual,archived");

            migrationBuilder.CreateTable(
                name: "CargoOrder",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    BodyKind = table.Column<CargoBodyKind>(type: "cargo_body_kind", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    DeliverRegion = table.Column<Region>(type: "region", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LoadType = table.Column<byte>(type: "cargo_load_type", nullable: true),
                    Loaders = table.Column<byte>(type: "cargo_loaders", nullable: true),
                    MaxBudget = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentMethod = table.Column<PaymentMethod>(type: "payment_method", nullable: true),
                    PickupRegion = table.Column<Region>(type: "region", nullable: false),
                    ShipmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    Title = table.Column<string>(type: "text", nullable: false),
                    TransportationType = table.Column<byte>(type: "cargo_transportation_type", nullable: true),
                    Type = table.Column<CargoType>(type: "cargo_type", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)")
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
                name: "PassengerOrder",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CountSeats = table.Column<int>(type: "integer", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    DeliverRegion = table.Column<Region>(type: "region", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Facilities = table.Column<PassengerFacilities>(type: "passenger_facilities", nullable: true),
                    MaxBudget = table.Column<decimal>(type: "numeric", nullable: false),
                    Option = table.Column<PassengerOption>(type: "passenger_option", nullable: true),
                    PaymentMethod = table.Column<PaymentMethod>(type: "payment_method", nullable: true),
                    PickupRegion = table.Column<Region>(type: "region", nullable: false),
                    RentalDuration = table.Column<PassengerRentalDuration>(type: "passenger_rental_duration", nullable: true),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    Title = table.Column<string>(type: "text", nullable: false),
                    TransportationKind = table.Column<PassengerTransportationKind>(type: "passenger_transportation_kind", nullable: true),
                    Type = table.Column<PassengerType>(type: "passenger_type", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)")
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
                name: "SpecialOrder",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    DeliverRegion = table.Column<Region>(type: "region", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    MaxBudget = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentMethod = table.Column<PaymentMethod>(type: "payment_method", nullable: true),
                    PickupRegion = table.Column<Region>(type: "region", nullable: false),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<SpecialType>(type: "special_type", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)")
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
                name: "CargoOrderImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
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
                name: "PassengerOrderImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
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
                name: "SpecialOrderImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
                    State = table.Column<State>(type: "state", nullable: false, defaultValueSql: "'actual'::state"),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', current_timestamp)"),
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

            migrationBuilder.CreateIndex(
                name: "IX_CargoOrder_UserId",
                table: "CargoOrder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoOrderImage_OrderId",
                table: "CargoOrderImage",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerOrder_UserId",
                table: "PassengerOrder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerOrderImage_OrderId",
                table: "PassengerOrderImage",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialOrder_UserId",
                table: "SpecialOrder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialOrderImage_OrderId",
                table: "SpecialOrderImage",
                column: "OrderId");
        }
    }
}
