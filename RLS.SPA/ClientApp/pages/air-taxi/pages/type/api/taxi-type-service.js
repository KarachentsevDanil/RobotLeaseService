import * as httpService from '../../../../../api/http-service'

const apiTaxiType = "/api/robottype/";

export const getTaxiTypesByParams = data => {
    let params = {
        url: apiTaxiType,
        data: data
    }

    return httpService.getData(params);
}

export const addTaxiType = data => {
    let params = {
        url: apiTaxiType,
        data: data
    }

    return httpService.postData(params);
}