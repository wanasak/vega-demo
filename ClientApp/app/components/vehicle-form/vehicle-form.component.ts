import * as _ from 'underscore';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { ToastyService } from "ng2-toasty";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/Observable/forkJoin';
import { SaveVehicle, Vehicle } from "../../models/vehicle";

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

  makes: any[];
  models: any[];
  features: any[];
  vehicle: SaveVehicle = {
    id: 0,
    makeId: 0,
    modelId: 0,
    isRegistered: false,
    features: [],
    contact: {
      name: '',
      email: '',
      phone: '',
    }
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private vehicleService: VehicleService,
    private toastyService: ToastyService) {
    route.params.subscribe(p => {
      this.vehicle.id = +p['id'] || 0;
    });
  }

  ngOnInit() {
    var sources = [
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures()
    ];

    if (this.vehicle.id > 0)
      sources.push(this.vehicleService.getVehicle(this.vehicle.id));

    Observable.forkJoin(sources)
      .subscribe(data => {
        this.makes = data[0];
        this.features = data[1];
        if (this.vehicle.id > 0) {
          this.setVehicle(data[2]);
          this.populateModels();
        }
      }, err => {
        if (err.status == 404)
          this.router.navigate(['/home']);
      });

    // if (this.vehicle.id > 0) {
    //   this.vehicleService.getVehicle(this.vehicle.id)
    //     .subscribe(
    //     vehicle => this.vehicle = vehicle,
    //     err => {
    //       if (err.status == 404)
    //         this.router.navigate(['/home']);
    //     });
    // }
    // this.vehicleService.getMakes()
    //   .subscribe(makes => this.makes = makes);
    // this.vehicleService.getFeatures()
    //   .subscribe(feature => this.features = feature);
  }

  private setVehicle(v: Vehicle) {
    this.vehicle.id = v.id;
    this.vehicle.makeId = v.make.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.contact = v.contact;
    this.vehicle.features = _.pluck(v.features, 'id');
  }

  private populateModels() {
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
  }

  onMakeChange() {
    this.populateModels();
    delete this.vehicle.modelId;
  }

  onFeatureToggle(featureId: number, $event) {
    if ($event.target.checked)
      this.vehicle.features.push(featureId);
    else {
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit() {
    // variables with $ is use to indicate that this is an observable
    var result$ = (this.vehicle.id) ? this.vehicleService.updateVehcle(this.vehicle) : this.vehicleService.createVehicle(this.vehicle);
    result$.subscribe(vehicle => {
      this.toastyService.success({
        title: 'Success',
        msg: 'The vehicle was successfully saved.',
        theme: 'bootstrap',
        showClose: true,
        timeout: 3000
      });
      this.router.navigate(['/vehicles/', vehicle.id]);
    });
    // if (this.vehicle.id > 0) {
    //   this.vehicleService.updateVehcle(this.vehicle)
    //     .subscribe(v => {
    //       this.toastyService.success({
    //         title: 'Success',
    //         msg: 'The vehicle was successfully updated.',
    //         theme: 'bootstrap',
    //         showClose: true,
    //         timeout: 3000
    //       });
    //     });
    // } else {
    //   this.vehicleService.createVehicle(this.vehicle)
    //     .subscribe(v => {
    //       this.router.navigate(['/vehicles']);
    //     });
    // }
  }

  delete(id) {
    if (confirm('Are you sure?')) {
      this.vehicleService.deleteVehicle(this.vehicle.id)
        .subscribe(v => {
          this.router.navigate(['/vehicles']);
        });
    }
  }

}
