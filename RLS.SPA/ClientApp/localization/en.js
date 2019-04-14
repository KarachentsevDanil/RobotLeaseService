export const enLocalization = {
    common: {
        language: "Language",
        airTaxies: "Robots",
        name: "Name",
        country: "Country",
        description: "Description",
        add: "Add",
        close: "Close",
        details: "Details",
        filter: "Filter",
        totalItems: "Total",
        itemsPages: "items / page"
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
        taxiType: "Robot Type:",
        modelDescription: "Description...",
        modelReleaseYear: "Release Year...",
        releaseYear: "Release Year:",
        flightRange: "Maximum Range Flight(KM):",
        modelFlightRange: "Maximum Range...",
        uploadPhoto: "Upload photo:",
        taxiModelAdded: "Robot model was successfully added.",
        taxiModelList: "Robot Model List"
    },
    taxi: {
        addAirTaxi: "Add Robot",
        company: "Company:",
        taxiType: "Robot Type:",
        taxiModel: "Robot Model:",
        pricePerDay: "Daily Costs:",
        airTaxiList: "Robot List",
        taxiAdded: "Robot was successfully added."
    },
    type: {
        addTaxiType: "Add Robot Type",
        taxiTypeList: "Robot Type List",
        typeName: "Type Name...",
        typeDescription: "Description...",
        taxiTypeAdded: "Robot type was successfully added."
    },
    rent: {
        rents: "Rents",
        rent: "Rent",
        rentList: "Rent List",
        from: "From:",
        to: "To:",
        totalCosts: "Total Costs:",
        rentTaxi: "Rent Robot",
        companies: "Companies:",
        models: "Robot Models:",
        types: "Robot Types:",
        startDate: "Start Date:",
        endDate: "End Date:",
        findTaxies: "Find Robots",
        owner: "Owner:",
        taxi: "Robot:",
        perDay: " per day.",
        company: "Company:",
        model: "Model:",
        type: "Type:",
        description: "Description:",
        capacity: "Capacity:",
        dailyCapacity: "Daily Costs:",
        taxiDetails: "Robot Details",
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
    taxiGrid: {
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
        taxiTitle: "Robot",
        ownerTitle: "Owner",
        customerTitle: "Customer",
        infoTitle: "Info",
        actionsTitle: "Actions"
    }
}

export const adminTaxiItems = [{
        title: "Robot",
        icon: "icon-users2",
        url: "",
        children: [{
                title: "Company List",
                url: "/companies"
            },
            {
                title: "Type List",
                url: "/taxi-types"
            },
            {
                title: "Model List",
                url: "/taxi-models"
            },
            {
                title: "Robot List",
                url: "/taxies"
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
export const userTaxiItems = [{
        title: "Robot",
        icon: "icon-users2",
        url: "",
        children: [{
                title: "Model List",
                url: "/taxi-models"
            },
            {
                title: "My Robot List",
                url: "/taxies"
            }
        ]
    },
    {
        title: "Rents",
        icon: "icon-cog3",
        url: "",
        children: [{
                title: "Rent Robot",
                url: "/rent-taxi"
            },
            {
                title: "My Rents List",
                url: "/rent-list"
            }
        ]
    }
]
