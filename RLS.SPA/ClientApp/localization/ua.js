export const uaLocalization = {
    common: {
        language: "Мова",
        airRobots: "Роботи",
        name: "Им'я",
        country: "Країна",
        description: "Опис",
        add: "Додати",
        close: "Закрити",
        details: "Деталі",
        filter: "Фільтер",
        totalItems: "Всього",
        itemsPages: "записи / сторинка",
        companyDashboard: "Панель приладів Компанії",
        modelDashboard: "Панель приладів Моделi",
        typeDashboard: "Панель приладів Типу",
        search: "Пошук...",
        clear: "Очистити",
        priceRange: "Цiновий Дiаназон:",
        ratingRange: "Рейтинговий Дiаназон:"
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
        phone: "Телефон",
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
        robotType: "Тип Робота:",
        modelDescription: "Опис...",
        modelReleaseYear: "Рiк Випуску...",
        releaseYear: "Рiк Випуску:",
        flightRange: "Максимальна відстань польоту:",
        modelFlightRange: "Максимальна відстань польоту...",
        uploadPhoto: "Загрузити фото:",
        robotModelAdded: "Модель робота була успішно додана.",
        robotModelList: "Список Моделей Роботів"
    },
    robot: {
        addRobot: "Додати Робота",
        company: "Компанія:",
        robotType: "Тип Робота:",
        robotModel: "Модель Робота:",
        pricePerDay: "Ціна в День:",
        airrobotList: "Список Літаючих Роботiв",
        robotAdded: "Робота було успішно додана.",
        avarageRating: "Середнiй Рейтинг:",
        feedbacks: "Вiдгуки"
    },
    type: {
        addrobotType: "Додати Тип Робота",
        robotTypeList: "Список Типів Роботів",
        typeName: "Ім'я...",
        typeDescription: "Опис...",
        robotTypeAdded: "Тип робота було успішно додана."
    },
    chart: {
        modelPieChartTitle: "Моделi Роботiв",
        modelPieChartSubText: "Моделi Роботiв, що мають бiльше всiх роботiв",
        modelPieChartName: "Модель Робота",
    },
    rent: {
        complete: "Пiдтвердити",
        decline: "Вiдклонити",
        rents: "Оренди",
        rent: "Орендувати",
        rentList: "Список Оренд",
        chat: "Чат",
        from: "З:",
        to: "До:",
        totalCosts: "Загальна Вартість:",
        rentrobot: "Орендувати Робота",
        companies: "Компанії:",
        models: "Моделі Роботiв:",
        types: "Типи Роботiв:",
        startDate: "Дата Початку:",
        endDate: "Дата Кінця:",
        findRobots: "Знайти Роботiв",
        ownerPhone: "Телефон Власника:",
        owner: "Власник:",
        robot: "Робот:",
        perDay: " за день.",
        feedback: "Залишити Вiдгук",
        feedbackLabel: "Вiдгук",
        search: "Пошук:",
        rentUpdatedMessage: "Статус оренди успiшно оновлено",
        feedbackLeavedMessage: "Вiдгук залишено",
        ratingLabel: "Рейтинг",
        company: "Компанія:",
        model: "Модель:",
        type: "Тип:",
        description: "Опис:",
        capacity: "Ємкість:",
        dailyCapacity: "Ціна в День:",
        rentDetails: "Деталі Оренди",
        robotDetails: "Деталі Таксі",
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
    robotGrid: {
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
        robotTitle: "Таксі",
        ownerTitle: "Власник",
        customerTitle: "Кліент",
        feedbackTitle: "Вiдгук",
        infoTitle: "Деталі",
        actionsTitle: "Команди"
    }
}

export const adminrobotItems = [
    {
        title: "Панель приладів",
        icon: "icon-home4",
        url: "",
        children: [{
            title: "Панель приладів додатку",
            url: "/dashboard"
        },
        {
            title: "Панель приладів Компанії",
            url: "/company-dashboard"
        },
        {
            title: "Панель приладів Типу",
            url: "/type-dashboard"
        },
        {
            title: "Панель приладів Моделi",
            url: "/model-dashboard"
        }
        ]
    },
    {
        title: "Роботи",
        icon: "icon-users2",
        url: "",
        children: [{
            title: "Компанії",
            url: "/companies"
        },
        {
            title: "Типи Роботиів",
            url: "/robot-types"
        },
        {
            title: "Моделі Роботиів",
            url: "/robot-models"
        },
        {
            title: "Роботи",
            url: "/robots"
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
export const userrobotItems = [
    {
        title: "Панель приладів",
        icon: "icon-home4",
        url: "",
        children: [{
            title: "Панель приладів додатку",
            url: "/dashboard"
        },
        {
            title: "Панель приладів Компанії",
            url: "/company-dashboard"
        },
        {
            title: "Панель приладів Типу",
            url: "/type-dashboard"
        },
        {
            title: "Панель приладів Моделi",
            url: "/model-dashboard"
        }
        ]
    },
    {
        title: "Роботи",
        icon: "icon-users2",
        url: "",
        children: [{
            title: "Моделі Роботиів",
            url: "/robot-models"
        },
        {
            title: "Мої Роботи",
            url: "/robots"
        }
        ]
    },
    {
        title: "Оренди",
        icon: "icon-cog3",
        url: "",
        children: [{
            title: "Орендувати Робота",
            url: "/rent-robot"
        },
        {
            title: "Мої Оренди",
            url: "/rent-list"
        },
        {
            title: "Оренди Моїх Роботiв",
            url: "/owner-rent-list"
        }
        ]
    }
]
