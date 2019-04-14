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