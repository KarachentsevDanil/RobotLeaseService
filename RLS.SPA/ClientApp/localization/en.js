export const enLocalization = {
    common: {
        language: "Language",
        airRobots: "Robots",
        name: "Name",
        country: "Country",
        description: "Description",
        add: "Add",
        close: "Close",
        details: "Details",
        filter: "Filter",
        totalItems: "Total",
        itemsPages: "items / page",
        companyDashboard: "Company Dashboard",
        modelDashboard: "Model Dashboard",
        typeDashboard: "Type Dashboard",
        search: "Search...",
        clear: "Clear",
        priceRange: "Price Range:",
        ratingRange: "Rating Range:"
    },
    authorization: {
        email: "E-mail",
        password: "Password",
        login: "Login",
        signUp: "Sign Up",
        createNewMessage: "Don't have an account ?",
        loginToAccount: "Login to your account",
        credentials: "Your credentials"
    },
    registration: {
        email: "E-mail",
        password: "Password",
        fullName: "Full Name",
        firstName: "First Name",
        lastName: "Last Name",
        phone: "Phone",
        createAccount: "Create an account",
        fieldsRequired: "All fields are required",
        doYouHaveAnAccount: "Do you have an account ?",
        singIn: "Sing In",
        registr: "Register"
    },
    companies: {
        companyList: "Comapny List",
        addCompany: "Add Company",
        companyName: "Company Name...",
        country: "Country...",
        description: "Description...",
        companyAdded: "Company was successfully added."
    },
    models: {
        addModel: "Add Model",
        modelName: "Model Name...",
        company: "Company:",
        robotType: "Robot Type:",
        modelDescription: "Description...",
        modelReleaseYear: "Release Year...",
        releaseYear: "Release Year:",
        flightRange: "Maximum Range Flight(KM):",
        modelFlightRange: "Maximum Range...",
        uploadPhoto: "Upload photo:",
        robotModelAdded: "Robot model was successfully added.",
        robotModelList: "Robot Model List"
    },
    robot: {
        addRobot: "Add Robot",
        company: "Company:",
        robotType: "Robot Type:",
        robotModel: "Robot Model:",
        pricePerDay: "Daily Costs:",
        airrobotList: "Robot List",
        robotAdded: "Robot was successfully added.",
        avarageRating: "Avarage Rating:",
        feedbacks: "Feedbacks"
    },
    type: {
        addrobotType: "Add Robot Type",
        robotTypeList: "Robot Type List",
        typeName: "Type Name...",
        typeDescription: "Description...",
        robotTypeAdded: "Robot type was successfully added."
    },
    chart: {
        model: {
            pieChartTitle: "Robot Models",
            pieChartSubText: "Models that have most of the robots",
            rentPieChartSubText: "Models that have most of the rents",
            pieChartName: "Robot Model",
        },
        type: {
            pieChartTitle: "Robot Types",
            pieChartSubText: "Types that have most of the robots",
            rentPieChartSubText: "Types that have most of the rents",
            pieChartName: "Robot Type",
        },
        company: {
            pieChartTitle: "Robot Companies",
            pieChartSubText: "Companies that have most of the robots",
            rentPieChartSubText: "Companies that have most of the rents",
            pieChartName: "Robot Company",
        }
    },
    rent: {
        complete: "Complete",
        decline: "Cancel",
        cancelReason: "Cancel Reason:",
        chat: "Chat",
        rents: "Rents",
        rent: "Rent",
        rentList: "Rent List",
        from: "From:",
        to: "To:",
        totalCosts: "Total Costs:",
        rentrobot: "Rent Robot",
        companies: "Companies:",
        models: "Robot Models:",
        types: "Robot Types:",
        startDate: "Start Date:",
        endDate: "End Date:",
        findRobots: "Find Robots",
        owner: "Owner:",
        ownerPhone: "Owner Phone:",
        robot: "Robot:",
        perDay: " per day.",
        company: "Company:",
        model: "Model:",
        search: "Search:",
        type: "Type:",
        feedback: "Leave Feedback",
        feedbackLabel: "Feedback",
        feedbackLeavedMessage: "Feedback successfully leaved",
        rentUpdatedMessage: "Rent status successfully updated",
        ratingLabel: "Rating",
        description: "Description:",
        capacity: "Capacity:",
        dailyCapacity: "Daily Costs:",
        robotDetails: "Robot Details",
        rentDetails: "Rent Details",
        rentAdded: "Rent has successfully added.",
        rentError: "Try to find another date range for rent, you can check calendar to find free date range."
    },
    companyGrid: {
        nameTitle: "Company Name",
        countryTitle: "Country",
        descriptionTitle: "Description",
        actionsTitle: "Actions"
    },
    modelGrid: {
        modelNameTitle: "Model Name",
        companyTitle: "Company",
        typeTitle: "Type",
        descriptionTitle: "Description",
        actionsTitle: "Actions"
    },
    robotGrid: {
        ownerTitle: "Owner",
        modelNameTitle: "Model Name",
        companyTitle: "Company",
        typeTitle: "Type",
        dailyCostsTitle: "Daily Costs",
        actionsTitle: "Actions"
    },
    typeGrid: {
        typeNameTitle: "Type Name",
        descriptionTitle: "Description",
        actionsTitle: "Actions"
    },
    rentGrid: {
        robotTitle: "Robot",
        ownerTitle: "Owner",
        customerTitle: "Customer",
        infoTitle: "Info",
        feedbackTitle: "Feedback",
        actionsTitle: "Actions"
    }
}

export const adminrobotItems = [
    {
        title: "Dashboards",
        icon: "icon-home4",
        url: "",
        children: [{
            title: "Company Dashboard",
            url: "/company-dashboard"
        },
        {
            title: "Type Dashboard",
            url: "/type-dashboard"
        },
        {
            title: "Model Dashboard",
            url: "/model-dashboard"
        }
        ]
    },
    {
        title: "Robot",
        icon: "icon-users2",
        url: "",
        children: [{
            title: "Company List",
            url: "/companies"
        },
        {
            title: "Type List",
            url: "/robot-types"
        },
        {
            title: "Model List",
            url: "/robot-models"
        },
        {
            title: "Robot List",
            url: "/robots"
        }
        ]
    },
    {
        title: "Rents",
        icon: "icon-cog3",
        url: "",
        children: [{
            title: "Rents List",
            url: "/rent-list"
        }]
    }
];
export const userrobotItems = [
    {
        title: "Dashboards",
        icon: "icon-home4",
        url: "",
        children: [{
            title: "Company Dashboard",
            url: "/company-dashboard"
        },
        {
            title: "Type Dashboard",
            url: "/type-dashboard"
        },
        {
            title: "Model Dashboard",
            url: "/model-dashboard"
        }
        ]
    },
    {
        title: "Robot",
        icon: "icon-users2",
        url: "",
        children: [{
            title: "Model List",
            url: "/robot-models"
        },
        {
            title: "My Robot List",
            url: "/robots"
        }
        ]
    },
    {
        title: "Rents",
        icon: "icon-cog3",
        url: "",
        children: [
            {
                title: "Rent Robot",
                url: "/rent-robot"
            },
            {
                title: "My Rent List",
                url: "/rent-list"
            },
            {
                title: "My Robot Rent List",
                url: "/owner-rent-list"
            }
        ]
    }
]
