export const uaLocalization = {
    common: {
        language: "Мова",
        airTaxies: "Роботи",
        name: "Им'я",
        country: "Країна",
        description: "Опис",
        add: "Додати",
        close: "Закрити",
        details: "Деталі",
        filter: "Фільтер",
        totalItems: "Всього",
        itemsPages: "записи / сторинка"
    },
    authorization: {
        email: "Електронна пошта",
        password: "Пароль",
        login: "Увійти",
        signUp: "Зареєструватися",
        createNewMessage: "Не маєте облікового запису?",
        loginToAccount: "Увійдіть до свого облікового запису",
        credentials: "Ваші облікові дані"
    },
    registration: {
        email: "Електронна пошта",
        password: "Пароль",
        fullName: "Повне ім'я",
        firstName: "Iм'я",
        lastName: "По батьковi",
        createAccount: "Створити обліковий запис",
        fieldsRequired: "Всі поля обов'язкові для заповнення",
        doYouHaveAnAccount: "У вас є обліковий запис?",
        singIn: "Увійти",
        registr: "Зареєструватися"
    },
    companies: {
        companyList: "Список Компаній",
        addCompany: "Додати Компанію",
        companyName: "Им'я...",
        country: "Країна...",
        description: "Опис...",
        companyAdded: "Компанія була успішно додана."
    },
    models: {
        addModel: "Додати Модель",
        modelName: "Ім'я...",
        company: "Компанія:",
        taxiType: "Тип Робота:",
        modelDescription: "Опис...",
        modelReleaseYear: "Рiк Випуску...",
        releaseYear: "Рiк Випуску:",
        flightRange: "Максимальна відстань польоту:",
        modelFlightRange: "Максимальна відстань польоту...",
        uploadPhoto: "Загрузити фото:",
        taxiModelAdded: "Модель робота була успішно додана.",
        taxiModelList: "Список Моделей Роботів"
    },
    taxi: {
        addAirTaxi: "Додати Робота",
        company: "Компанія:",
        taxiType: "Тип Робота:",
        taxiModel: "Модель Робота:",
        pricePerDay: "Ціна в День:",
        airTaxiList: "Список Літаючих Роботiв",
        taxiAdded: "Робота було успішно додана."
    },
    type: {
        addTaxiType: "Додати Тип Робота",
        taxiTypeList: "Список Типів Роботів",
        typeName: "Ім'я...",
        typeDescription: "Опис...",
        taxiTypeAdded: "Тип робота було успішно додана."
    },
    rent: {
        rents: "Оренди",
        rent: "Орендувати",
        rentList: "Список Оренд",
        from: "З:",
        to: "До:",
        totalCosts: "Загальна Вартість:",
        rentTaxi: "Орендувати Робота",
        companies: "Компанії:",
        models: "Моделі Роботiв:",
        types: "Типи Роботiв:",
        startDate: "Дата Початку:",
        endDate: "Дата Кінця:",
        findTaxies: "Знайти Роботiв",
        owner: "Власник:",
        taxi: "Робот:",
        perDay: " за день.",
        feedback: "Залишити Вiдгук",
        feedbackLabel: "Вiдгук",
        search: "Пошук:",
        feedbackLeavedMessage: "Вiдгук залишено",
        ratingLabel: "Рейтинг",
        company: "Компанія:",
        model: "Модель:",
        type: "Тип:",
        description: "Опис:",
        capacity: "Ємкість:",
        dailyCapacity: "Ціна в День:",
        rentDetails: "Деталі Оренди",
        taxiDetails: "Деталі Таксі",
        rentAdded: "Оренда була успішно додана.",
        rentError: "Спробуйте знайти іншу дату для оренди."
    },
    companyGrid: {
        nameTitle: "Назва Компанії",
        countryTitle: "Країна",
        descriptionTitle: "Опис",
        actionsTitle: "Команди"
    },
    modelGrid: {
        modelNameTitle: "Назва Моделі",
        companyTitle: "Компанія",
        typeTitle: "Тип",
        descriptionTitle: "Опис",
        actionsTitle: "Команди"
    },
    taxiGrid: {
        ownerTitle: "Власник",
        modelNameTitle: "Назва Моделі",
        companyTitle: "Компанія",
        typeTitle: "Тип",
        dailyCostsTitle: "Ціна в День",
        actionsTitle: "Команди"
    },
    typeGrid: {
        typeNameTitle: "Назва Типу",
        descriptionTitle: "Опис",
        actionsTitle: "Команди"
    },
    rentGrid: {
        taxiTitle: "Таксі",
        ownerTitle: "Власник",
        customerTitle: "Кліент",
        infoTitle: "Деталі",
        actionsTitle: "Команди"
    }
}

export const adminTaxiItems = [{
        title: "Роботи",
        icon: "icon-users2",
        url: "",
        children: [{
                title: "Компанії",
                url: "/companies"
            },
            {
                title: "Типи Роботиів",
                url: "/taxi-types"
            },
            {
                title: "Моделі Роботиів",
                url: "/taxi-models"
            },
            {
                title: "Роботи",
                url: "/taxies"
            }
        ]
    },
    {
        title: "Оренди",
        icon: "icon-cog3",
        url: "",
        children: [{
            title: "Оренди",
            url: "/rent-list"
        }]
    }
];
export const userTaxiItems = [{
        title: "Роботи",
        icon: "icon-users2",
        url: "",
        children: [{
                title: "Моделі Роботиів",
                url: "/taxi-models"
            },
            {
                title: "Мої Роботи",
                url: "/taxies"
            }
        ]
    },
    {
        title: "Оренди",
        icon: "icon-cog3",
        url: "",
        children: [{
                title: "Орендувати Робота",
                url: "/rent-taxi"
            },
            {
                title: "Мої Оренди",
                url: "/rent-list"
            }
        ]
    }
]
