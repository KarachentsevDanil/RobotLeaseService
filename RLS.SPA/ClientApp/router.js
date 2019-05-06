import Vue from "vue";
import VueRouter from "vue-router";
import LoginPage from "./pages/auth/pages/login";

import authorizationRoutes from "./routes/authorization-routes";
import airrobotRoutes from "./routes/air-robot-routes";
import rentRoutes from "./routes/rent-routes";

Vue.use(VueRouter);

const routes = [
    ...authorizationRoutes,
    ...airrobotRoutes,
    ...rentRoutes,
    {
        path: "*",
        component: LoginPage
    }
];

let router = new VueRouter({
    mode: "history",
    routes
});

export default router;
