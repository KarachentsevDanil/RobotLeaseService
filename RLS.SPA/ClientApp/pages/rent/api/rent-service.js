import * as httpService from '../../../api/http-service'

const apiRent = "/api/rental/";

export const getRentsByParams = data => {
    let params = {
        url: apiRent,
        data: data
    }

    return httpService.getData(params);
}

export const getRentById = id => {
    let params = {
        url: apiRent + id
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

export const customerUpdateRent = data => {
    let params = {
        url: apiRent + "customer",
        data: data
    }

    return httpService.putData(params);
}

export const ownerUpdateRent = data => {
    let params = {
        url: apiRent + "owner",
        data: data
    }

    return httpService.putData(params);
}

export const updateRent = data => {
    let params = {
        url: apiRent,
        data: data
    }

    return httpService.putData(params);
}