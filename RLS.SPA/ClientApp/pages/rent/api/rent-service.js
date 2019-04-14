import * as httpService from '../../../api/http-service'

const apiRent = "/api/rental/";

export const getRentsByParams = data => {
    let params = {
        url: apiRent,
        data: data
    }

    return httpService.getData(params);
}

export const addRent = data => {
    let params = {
        url: apiRent,
        data: data
    }

    return httpService.postData(params);
}