import * as httpService from '../../../../../api/http-service'

const apirobotType = "/api/robottype/";

export const getrobotTypesByParams = data => {
    let params = {
        url: apirobotType,
        data: data
    }

    return httpService.getData(params);
}

export const addrobotType = data => {
    let params = {
        url: apirobotType,
        data: data
    }

    return httpService.postData(params);
}

export const getTopRobotTypesByRobotCount = data => {
    let params = {
        url: apirobotType + "chart/robot",
        data: data
    }

    return httpService.getData(params);
}

export const getTopRobotTypesByRentCount = data => {
    let params = {
        url: apirobotType + "chart/rents",
        data: data
    }

    return httpService.getData(params);
}

export const getTopRobotTypesByRobotAndRentsCount = data => {
    let params = {
        url: apirobotType + "bar-chart/all",
        data: data
    }

    return httpService.getData(params);
}