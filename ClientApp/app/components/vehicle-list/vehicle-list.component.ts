import { Component, OnInit } from '@angular/core';
import { VehicleService } from "../../services/vehicle.service";
import { Vehicle, KeyPairValue } from "../../models/vehicle";

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html'
})
export class VehicleListComponent implements OnInit {

  private readonly PAGE_SIZE = 3;
  queryResult: any = {};
  makes: any[];
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  columns = [
    { title: 'Id' },
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    { title: 'Contact Name', key: 'contactName', isSortable: true }
  ];

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMakes()
      .subscribe(makes => this.makes = makes);

    this.populateVehicles();
  }

  private populateVehicles() {
    this.vehicleService.getVehicles(this.query)
      .subscribe(result => {
        this.queryResult = result;
      });
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
    this.query.page = 1;
    this.populateVehicles();
  }

  resetFilter() {
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
    // this.onFilterChange();
    this.populateVehicles();
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateVehicles();
  }

  onPageChange(page) {
    this.query.page = page;
    this.populateVehicles();
  }

}
