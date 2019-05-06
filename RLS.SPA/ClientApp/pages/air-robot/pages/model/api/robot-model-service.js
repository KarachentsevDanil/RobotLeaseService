import * as httpService from '../../../../../api/http-service'

const apirobotModel = "/api/robotmodel/";
const apirobotCompany = "/api/robotcompany/";
const apirobotType = "/api/robottype/";

export const getrobotModelsByParams = data => {
    let params = {
        url: apirobotModel,
        data: data
    }

    return httpService.getData(params);
}

export const getAirrobotCompanies = data => {
    let params = {
        url: apirobotCompany + "robot-companies",
        data: data
    }

    return httpService.getData(params);
}

export const getAirrobotTypes = data => {
    let params = {
        url: apirobotType + "robot-types",
        data: data
    }

    return httpService.getData(params);
}

export const addrobotModel = data => {
    let params = {
        url: apirobotModel,
        data: data
    }

    return httpService.postData(params);
}

export const getTopRobotModelsByRobotCount = data => {
    let params = {
        url: apirobotModel + "chart/robot",
        data: data
    }

    return httpService.getData(params);
}

export const getTopRobotModelsByRentCount = data => {
    let params = {
        url: apirobotModel + "chart/rents",
        data: data
    }

    return httpService.getData(params);
}

export const getTopRobotModelsByRobotAndRentsCount = data => {
    let params = {
        url: apirobotModel + "bar-chart/all",
        data: data
    }

    return httpService.getData(params);
}