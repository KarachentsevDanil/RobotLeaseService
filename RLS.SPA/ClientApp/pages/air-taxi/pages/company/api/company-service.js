import * as httpService from '../../../../../api/http-service'

const apiCompany = "/api/robotcompany/";

export const getCompaniesByParams = data => {
    let params = {
        url: apiCompany,
        data: data
    }

    return httpService.getData(params);
}

export const addCompany = data => {
    let params = {
        url: apiCompany,
        data: data
    }

    return httpService.postData(params);
}

export const getTopRobotCompaniesByRobotCount = data => {
    let params = {
        url: apiCompany + "chart/robot",
        data: data
    }

    return httpService.getData(params);
}

export const getTopRobotCompaniesByRentCount = data => {
    let params = {
        url: apiCompany + "chart/rents",
        data: data
    }

    return httpService.getData(params);
}

export const getTopRobotCompaniesByRobotAndRentsCount = data => {
    let params = {
        url: apiCompany + "bar-chart/all",
        data: data
    }

    return httpService.getData(params);
}