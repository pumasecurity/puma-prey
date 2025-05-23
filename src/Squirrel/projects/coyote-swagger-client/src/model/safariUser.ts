/**
 * Puma Prey APIs
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { PumaUser } from './pumaUser';
import { Safari } from './safari';

export interface SafariUser { 
    id?: number;
    safariId?: number;
    safaris?: Safari;
    pumaUserId?: string;
    pumaUser?: PumaUser;
}