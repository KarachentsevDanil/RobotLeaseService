import * as authenticationService from "../api/authentication-service";
import * as mutations from "./types/mutators-types";
import * as authResources from "../resources/resources";

export default {
    async login(context, data) {
        let userDate = (await authenticationService.login(data.user)).data.Data;
        
        if (!userDate.ErrorMessage) {
            let token = {
                value: userDate.token,
                expireData: userDate.tokenExpireData
            };

            context.commit(mutations.SET_TOKEN_MUTATOR, token);
            context.commit(mutations.SET_USER_MUTATOR, userDate.User);
            data.router.push("/robots");
        } else {
            data.notification.error(
                authResources.popupMessages.loginFailedMessage
            );
        }
    },
    logout(context) {
        context.commit(mutations.USER_LOGOUT_MUTATOR);
    }
};
