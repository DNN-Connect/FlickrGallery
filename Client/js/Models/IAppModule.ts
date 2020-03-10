import { IContextSecurity } from "./index";
import DataService from "../Service";

export interface IAppModule {
    moduleId: number;
    tabId: number;
    locale: string;
    resources: any;
    viewType: string;
    security: IContextSecurity;
    service: DataService;
}

export class AppModule implements IAppModule {
    public moduleId: number;
    public tabId: number;
    public locale: string;
    public resources: any;
    public viewType: string;
    public security: IContextSecurity;
    public service: any;
    constructor(moduleId: number, tabId: number, locale: string, resources: any, viewType: string, security: IContextSecurity, service: DataService) {
        this.moduleId = moduleId;
        this.tabId = tabId;
        this.locale = locale;
        this.resources = resources;
        this.viewType = viewType;
        this.security = security;
        this.service = service;
    }
}
