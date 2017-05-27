import { NgZone } from '@angular/core';
import { ProgressService } from './../../services/progress.service';
import { ToastyService } from 'ng2-toasty';
import { PhotoService } from './../../services/photo.service';
import { VehicleService } from './../../services/vehicle.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { ElementRef, Component, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-vehicle-view',
  templateUrl: './vehicle-view.component.html'
})
export class VehicleViewComponent implements OnInit {
  @ViewChild('fileInput') fileInput: ElementRef;
  vehicle: any;
  vehicleId: number;
  photos = [];
  progress: any;

  constructor(
    private ngZone: NgZone,
    private route: ActivatedRoute,
    private router: Router,
    private toastyService: ToastyService,
    private vehicleService: VehicleService,
    private photoService: PhotoService,
    private progressService: ProgressService
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
    this.photoService.getPhotos(this.vehicleId)
      .subscribe(photos => this.photos = photos);

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

  uploadPhoto() {
    this.progressService.startTracking()
      .subscribe(progress => {
        console.log(progress);
        this.ngZone.run(() => this.progress = progress);
      },
      null,
      () => this.progress = null);

    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
    var file = nativeElement.files[0];
    this.photoService.upload(this.vehicleId, file)
      .subscribe(photo => {
        this.toastyService.success({
          title: 'Success',
          msg: 'The photo was successfully saved.',
          theme: 'bootstrap',
          showClose: true,
          timeout: 3000
        });
        this.photos.push(photo);
        nativeElement.value = '';
      }, err => {
        this.toastyService.error({
          title: 'Error',
          msg: err.text(),
          theme: 'bootstrap',
          showClose: true,
          timeout: 3000
        });
      });
  }

}
