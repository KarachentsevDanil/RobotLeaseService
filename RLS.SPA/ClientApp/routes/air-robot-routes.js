import CompanyListPage from "../pages/air-robot/pages/company/list/company-list";
import robotModelListPage from "../pages/air-robot/pages/model/list/robot-model-list";
import robotListPage from "../pages/air-robot/pages/robot/list/robot-list";
import robotTypeListPage from "../pages/air-robot/pages/type/list/robot-type-list";
import CompanyDashboardPage from "../pages/dashboard/pages/company-dashboard";
import ModelDashboardPage from "../pages/dashboard/pages/model-dashboard";
import TypeDashboardPage from "../pages/dashboard/pages/type-dashboard";

import * as routeGuards from "./route-guards";

export default [
    {
        path: "/companies",
        component: CompanyListPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    },
    {
        path: "/robot-models",
        component: robotModelListPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    },
    {
        path: "/robots",
        component: robotListPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    },
    {
        path: "/robot-types",
        component: robotTypeListPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    },
    {
        path: "/company-dashboard",
        component: CompanyDashboardPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    },
    {
        path: "/model-dashboard",
        component: ModelDashboardPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    },
    {
        path: "/type-dashboard",
        component: TypeDashboardPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    }
];
