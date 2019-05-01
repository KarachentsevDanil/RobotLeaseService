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

export const getTopRobotTypesByRobotCount = data => {
    let params = {
        url: apiTaxiType + "chart/robot",
        data: data
    }

    return httpService.getData(params);
}

export const getTopRobotTypesByRentCount = data => {
    let params = {
        url: apiTaxiType + "chart/rents",
        data: data
    }

    return httpService.getData(params);
}