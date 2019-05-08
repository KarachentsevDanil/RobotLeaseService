import * as httpService from '../../../../../api/http-service'

const apirobotModel = "/api/robotmodel/";
const apirobotCompany = "/api/robotcompany/";
const apirobotType = "/api/robottype/";
const apirobot = "/api/robot/";

export const getAirrobotById = id => {
    let params = {
        url: apirobot + id
    }

    return httpService.getData(params);
}

export const getStatistics = () => {
    let params = {
        url: apirobot + "statistic"
    }

    return httpService.getData(params);
}

export const getAirrobotModels = () => {
    let params = {
        url: apirobotModel + "models"
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

export const getRobotsByParams = data => {
    let params = {
        url: apirobot + "list",
        data: data
    }

    return httpService.postData(params);
}

export const getValuableRobotsByParams = data => {
    let params = {
        url: apirobot + "valuable",
        data: data
    }

    return httpService.postData(params);
}

export const addRobot = data => {
    let params = {
        url: apirobot,
        data: data
    }

    return httpService.postData(params);
}