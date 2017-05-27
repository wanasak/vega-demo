import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';

@Injectable()
export class PhotoService {

    constructor(private http: Http) { }

    upload(vehicleId, photo) {
        var formData = new FormData();
        formData.append('file', photo);
        return this.http.post(`/api/vehicles/${vehicleId}/photos`, formData)
            .map(res => res.json());
    }

}