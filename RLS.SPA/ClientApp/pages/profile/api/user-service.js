import * as httpService from "../../../api/http-service";

const userUrl = "/api/account/"

export const updateUser = data => {
    let params = {
        url: userUrl,
        data: data
    };

    return httpService.putData(params);
};
