import { VehicleService } from './../../services/vehicle.service';
import { ToastyModule } from 'ng2-toasty';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-view',
  templateUrl: './vehicle-view.component.html'
})
export class VehicleViewComponent implements OnInit {
  vehicle: any;
  vehicleId: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastyService: ToastyModule,
    private vehicleService: VehicleService
  ) {
    route.params.subscribe(p => {
      this.vehicleId = +p['id'];
      if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
        router.navigate(['/vehicles']);
        return;
      }
    });
  }

  ngOnInit() {
    this.vehicleService.getVehicle(this.vehicleId)
      .subscribe(
        vehicle => this.vehicle = vehicle,
        err => {
          if (err.status == 404) {
            this.router.navigate(['/vehicles']);
            return; 
          }
        });
  }

  delete() {
    if (confirm('Are you sure?')) {
      this.vehicleService.deleteVehicle(this.vehicle.id)
        .subscribe(v => {
          this.router.navigate(['/vehicles']);
        });
    }
  }

}
