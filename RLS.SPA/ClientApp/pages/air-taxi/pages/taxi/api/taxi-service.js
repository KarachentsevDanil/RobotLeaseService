import * as httpService from '../../../../../api/http-service'

const apiTaxiModel = "/api/robotmodel/";
const apiTaxiCompany = "/api/robotcompany/";
const apiTaxiType = "/api/robottype/";
const apiTaxi = "/api/robot/";

export const getAirTaxiById = id => {
    let params = {
        url: apiTaxi + id
    }

    return httpService.getData(params);
}

export const getAirTaxiModels = () => {
    let params = {
        url: apiTaxiModel + "models"
    }

    return httpService.getData(params);
}

export const getAirTaxiCompanies = data => {
    let params = {
        url: apiTaxiCompany + "robot-companies",
        data: data
    }

    return httpService.getData(params);
}

export const getAirTaxiTypes = data => {
    let params = {
        url: apiTaxiType + "robot-types",
        data: data
    }

    return httpService.getData(params);
}

export const getTaxiesByParams = data => {
    let params = {
        url: apiTaxi + "list",
        data: data
    }

    return httpService.postData(params);
}

export const addAirTaxi = data => {
    let params = {
        url: apiTaxi,
        data: data
    }

    return httpService.postData(params);
}