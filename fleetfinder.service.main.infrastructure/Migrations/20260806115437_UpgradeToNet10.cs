using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fleetfinder.service.main.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpgradeToNet10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                .OldAnnotation("Npgsql:Enum:cargo_body_kind", "auto_train,beam_vehicle,flatbed_vehicle,hydraulic_lift,cargo_passenger,oversized_vehicle,isothermal_vehicle,container_vehicle,feed_vehicle,flour_vehicle,open_vehicle,panel_vehicle,pickup,pyramid,semi_trailer,light_trailer,refrigerator,roll_vehicle,tractor_unit,agricultural_grain_vehicle,livestock_vehicle,timber_vehicle,glass_vehicle,coupling,tank_container,curtain_sider,insulated_vehicle,pipe_vehicle,trailer_truck,van,solid_metal,chip_car")
                .OldAnnotation("Npgsql:Enum:cargo_load_type", "top,rear,side,with_hydroboard,with_full_liftgate,with_ramps_or_discharges,with_tarmac,with_stakes,with_air_suspension")
                .OldAnnotation("Npgsql:Enum:cargo_loaders", "without_movers,one_movers,two_movers,three_movers")
                .OldAnnotation("Npgsql:Enum:cargo_transportation_kind", "cargo_taxi,apartment_moving,office_moving,furniture_transport,food_transport,intercity_transport,international_transport,lcl_transport,livestock_transport,personal_items_transport,appliance_transport,fruit_transport,vegetable_transport,country_house_moving,warehouse_moving,piano_transport,safe_transport,refrigerator_transport,equipment_transport,construction_materials_transport,motorcycle_transport")
                .OldAnnotation("Npgsql:Enum:cargo_transportation_type", "full_load,partial_load,overload")
                .OldAnnotation("Npgsql:Enum:cargo_type", "t1,t2,t3,t5,t10,t20")
                .OldAnnotation("Npgsql:Enum:experience_work", "less_year1,year1,year2,year3,year4,year5,year6,year7,year8,year9,more_year10")
                .OldAnnotation("Npgsql:Enum:passenger_facilities", "comfortable,economy,standard")
                .OldAnnotation("Npgsql:Enum:passenger_option", "audio_system_and_microphone,air_conditioner,ventilation_system,large_luggage_compartments,monitor_and_dvd,individual_lighting,seat_heating,spacious_salon,safety_belts,climate_control,toilet,sleeping_places")
                .OldAnnotation("Npgsql:Enum:passenger_rental_duration", "per_day,one_day,per_hour,per_month,long_term")
                .OldAnnotation("Npgsql:Enum:passenger_transportation_kind", "order,children,tourists,intercity,abroad_trips,corporate,airport_transfer,employee_delivery,wedding,vip,medical,funeral,pet,car_home,excursion,party_bus,courier")
                .OldAnnotation("Npgsql:Enum:passenger_type", "taxi,limousine,minivan,bus,shiftw,water")
                .OldAnnotation("Npgsql:Enum:payment_method", "cash,non_cash,cash_and_non_cash,card_payment")
                .OldAnnotation("Npgsql:Enum:payment_order", "prepayment,payment_upon_delivery,installment_payment")
                .OldAnnotation("Npgsql:Enum:region", "bender,tiraspol,grigoriopol,dubasari,camenka,ribnitsa,slobozia")
                .OldAnnotation("Npgsql:Enum:special_type", "excavator,aerial_platform,truck_crane,truck_fuel,bulldozer,hydra_hammer,grader,crag_loader,road_roller,mini_loader,mini_excavator,waste_car,dump_truck,tractor,front_loader,truck_cement,excavator_loader,yamobur")
                .OldAnnotation("Npgsql:Enum:state", "actual,archived");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
