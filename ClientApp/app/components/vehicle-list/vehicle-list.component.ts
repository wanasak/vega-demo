import { Component, OnInit } from '@angular/core';
import { VehicleService } from "../../services/vehicle.service";
import { Vehicle, KeyPairValue } from "../../models/vehicle";

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html'
})
export class VehicleListComponent implements OnInit {

  vehicles: Vehicle[];
  allVehicles: Vehicle[];
  makes: any[];
  filter: any = {};

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMakes()
      .subscribe(makes => this.makes = makes);

    this.populateVehicles();
  }

  private populateVehicles() {
    this.vehicleService.getVehicles(this.filter)
      .subscribe(vehicles => this.vehicles = this.allVehicles = vehicles);
  }

  onFilterChange() {
    // Client side filter
    // var vehicles = this.allVehicles;
    // if (this.filter.makeId)
    //   vehicles = vehicles.filter(v => v.make.id == this.filter.makeId);
    // if (this.filter.modelId)
    //   vehicles = vehicles.filter(v => v.model.id == this.filter.model);
    // this.vehicles = vehicles;

    // Server side filter
    this.populateVehicles();
  }

  resetFilter() {
    this.filter = {};
    this.onFilterChange();
  }

}
