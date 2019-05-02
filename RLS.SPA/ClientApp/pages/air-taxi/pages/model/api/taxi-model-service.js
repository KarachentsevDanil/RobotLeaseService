import * as httpService from '../../../../../api/http-service'

const apiTaxiModel = "/api/robotmodel/";
const apiTaxiCompany = "/api/robotcompany/";
const apiTaxiType = "/api/robottype/";

export const getTaxiModelsByParams = data => {
    let params = {
        url: apiTaxiModel,
        data: data
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

export const addTaxiModel = data => {
    let params = {
        url: apiTaxiModel,
        data: data
    }

    return httpService.postData(params);
}

export const getTopRobotModelsByRobotCount = data => {
    let params = {
        url: apiTaxiModel + "chart/robot",
        data: data
    }

    return httpService.getData(params);
}

export const getTopRobotModelsByRentCount = data => {
    let params = {
        url: apiTaxiModel + "chart/rents",
        data: data
    }

    return httpService.getData(params);
}

export const getTopRobotModelsByRobotAndRentsCount = data => {
    let params = {
        url: apiTaxiModel + "bar-chart/all",
        data: data
    }

    return httpService.getData(params);
}