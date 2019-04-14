import CompanyListPage from "../pages/air-taxi/pages/company/list/company-list";
import TaxiModelListPage from "../pages/air-taxi/pages/model/list/taxi-model-list";
import TaxiListPage from "../pages/air-taxi/pages/taxi/list/taxi-list";
import TaxiTypeListPage from "../pages/air-taxi/pages/type/list/taxi-type-list";

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
        path: "/taxi-models",
        component: TaxiModelListPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    },
    {
        path: "/taxies",
        component: TaxiListPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    },
    {
        path: "/taxi-types",
        component: TaxiTypeListPage,
        beforeEnter: (to, from, next) => {
            routeGuards.validateRoute(to, from, next);
        }
    }
];
