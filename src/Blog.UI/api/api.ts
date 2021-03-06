// tslint:disable
/// <reference path="./custom.d.ts" />
/**
 * My Blog
 * My personal blog
 *
 * OpenAPI spec version: 1.0
 *
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import * as url from "url";
import { Configuration } from "./configuration";
import globalAxios, { AxiosPromise, AxiosInstance } from "axios";

const BASE_PATH = "http://localhost:5000".replace(/\/+$/, "");

/**
 *
 * @export
 */
export const COLLECTION_FORMATS = {
  csv: ",",
  ssv: " ",
  tsv: "\t",
  pipes: "|"
};

/**
 *
 * @export
 * @interface RequestArgs
 */
export interface RequestArgs {
  url: string;
  options: any;
}

/**
 *
 * @export
 * @class BaseAPI
 */
export class BaseAPI {
  protected configuration: Configuration | undefined;

  constructor(
    configuration?: Configuration,
    protected basePath: string = BASE_PATH,
    protected axios: AxiosInstance = globalAxios
  ) {
    if (configuration) {
      this.configuration = configuration;
      this.basePath = configuration.basePath || this.basePath;
    }
  }
}

/**
 *
 * @export
 * @class RequiredError
 * @extends {Error}
 */
export class RequiredError extends Error {
  name: "RequiredError" = "RequiredError";
  constructor(public field: string, msg?: string) {
    super(msg);
  }
}

/**
 *
 * @export
 * @interface AdminRequest
 */
export interface AdminRequest {
  /**
   *
   * @type {string}
   * @memberof AdminRequest
   */
  userName?: string;
  /**
   *
   * @type {string}
   * @memberof AdminRequest
   */
  password?: string;
}

/**
 *
 * @export
 * @interface PostDto
 */
export interface PostDto {
  /**
   *
   * @type {number}
   * @memberof PostDto
   */
  id: number;
  /**
   *
   * @type {string}
   * @memberof PostDto
   */
  content?: string;
  /**
   *
   * @type {string}
   * @memberof PostDto
   */
  title?: string;
}

/**
 *
 * @export
 * @interface PostPreviewDto
 */
export interface PostPreviewDto {
  /**
   *
   * @type {number}
   * @memberof PostPreviewDto
   */
  id: number;
  /**
   *
   * @type {string}
   * @memberof PostPreviewDto
   */
  title?: string;
  /**
   *
   * @type {string}
   * @memberof PostPreviewDto
   */
  excerpt?: string;
}

/**
 *
 * @export
 * @interface PostRequest
 */
export interface PostRequest {
  /**
   *
   * @type {string}
   * @memberof PostRequest
   */
  content?: string;
  /**
   *
   * @type {string}
   * @memberof PostRequest
   */
  title?: string;
  /**
   *
   * @type {string}
   * @memberof PostRequest
   */
  excerpt?: string;
}

/**
 *
 * @export
 * @interface PreviewDto
 */
export interface PreviewDto {
  /**
   *
   * @type {string}
   * @memberof PreviewDto
   */
  content?: string;
}

/**
 *
 * @export
 * @interface PreviewRequest
 */
export interface PreviewRequest {
  /**
   *
   * @type {string}
   * @memberof PreviewRequest
   */
  content?: string;
}

/**
 * A machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807.
 * @export
 * @interface ProblemDetails
 */
export interface ProblemDetails {
  [key: string]: any | any;

  /**
   * A URI reference [RFC3986] that identifies the problem type. This specification encourages that, when dereferenced, it provide human-readable documentation for the problem type (e.g., using HTML [W3C.REC-html5-20141028]).  When this member is not present, its value is assumed to be \"about:blank\".
   * @type {string}
   * @memberof ProblemDetails
   */
  type?: string;
  /**
   * A short, human-readable summary of the problem type.It SHOULD NOT change from occurrence to occurrence of the problem, except for purposes of localization(e.g., using proactive content negotiation; see[RFC7231], Section 3.4).
   * @type {string}
   * @memberof ProblemDetails
   */
  title?: string;
  /**
   * The HTTP status code([RFC7231], Section 6) generated by the origin server for this occurrence of the problem.
   * @type {number}
   * @memberof ProblemDetails
   */
  status?: number;
  /**
   * A human-readable explanation specific to this occurrence of the problem.
   * @type {string}
   * @memberof ProblemDetails
   */
  detail?: string;
  /**
   * A URI reference that identifies the specific occurrence of the problem.It may or may not yield further information if dereferenced.
   * @type {string}
   * @memberof ProblemDetails
   */
  instance?: string;
}

/**
 * AdminApi - axios parameter creator
 * @export
 */
export const AdminApiAxiosParamCreator = function(
  configuration?: Configuration
) {
  return {
    /**
     *
     * @param {AdminRequest} adminRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    adminLogin(adminRequest: AdminRequest, options: any = {}): RequestArgs {
      // verify required parameter 'adminRequest' is not null or undefined
      if (adminRequest === null || adminRequest === undefined) {
        throw new RequiredError(
          "adminRequest",
          "Required parameter adminRequest was null or undefined when calling adminLogin."
        );
      }
      const localVarPath = `/api/Admin/login`;
      const localVarUrlObj = url.parse(localVarPath, true);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }
      const localVarRequestOptions = Object.assign(
        { method: "POST" },
        baseOptions,
        options
      );
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication JWT required
      if (configuration && configuration.apiKey) {
        const localVarApiKeyValue =
          typeof configuration.apiKey === "function"
            ? configuration.apiKey("Authorization")
            : configuration.apiKey;
        localVarHeaderParameter["Authorization"] = localVarApiKeyValue;
      }

      localVarHeaderParameter["Content-Type"] = "application/json";

      localVarUrlObj.query = Object.assign(
        {},
        localVarUrlObj.query,
        localVarQueryParameter,
        options.query
      );
      // fix override query string Detail: https://stackoverflow.com/a/7517673/1077943
      delete localVarUrlObj.search;
      localVarRequestOptions.headers = Object.assign(
        {},
        localVarHeaderParameter,
        options.headers
      );
      const needsSerialization =
        localVarRequestOptions.headers["Content-Type"] === "application/json";
      localVarRequestOptions.data = needsSerialization
        ? JSON.stringify(adminRequest || {})
        : adminRequest || "";

      return {
        url: url.format(localVarUrlObj),
        options: localVarRequestOptions
      };
    }
  };
};

/**
 * AdminApi - functional programming interface
 * @export
 */
export const AdminApiFp = function(configuration?: Configuration) {
  return {
    /**
     *
     * @param {AdminRequest} adminRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    adminLogin(
      adminRequest: AdminRequest,
      options?: any
    ): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<string> {
      const localVarAxiosArgs = AdminApiAxiosParamCreator(
        configuration
      ).adminLogin(adminRequest, options);
      return (
        axios: AxiosInstance = globalAxios,
        basePath: string = BASE_PATH
      ) => {
        const axiosRequestArgs = Object.assign(localVarAxiosArgs.options, {
          url: basePath + localVarAxiosArgs.url
        });
        return axios.request(axiosRequestArgs);
      };
    }
  };
};

/**
 * AdminApi - factory interface
 * @export
 */
export const AdminApiFactory = function(
  configuration?: Configuration,
  basePath?: string,
  axios?: AxiosInstance
) {
  return {
    /**
     *
     * @param {AdminRequest} adminRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    adminLogin(adminRequest: AdminRequest, options?: any) {
      return AdminApiFp(configuration).adminLogin(adminRequest, options)(
        axios,
        basePath
      );
    }
  };
};

/**
 * AdminApi - object-oriented interface
 * @export
 * @class AdminApi
 * @extends {BaseAPI}
 */
export class AdminApi extends BaseAPI {
  /**
   *
   * @param {AdminRequest} adminRequest
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof AdminApi
   */
  public adminLogin(adminRequest: AdminRequest, options?: any) {
    return AdminApiFp(this.configuration).adminLogin(adminRequest, options)(
      this.axios,
      this.basePath
    );
  }
}

/**
 * PostsApi - axios parameter creator
 * @export
 */
export const PostsApiAxiosParamCreator = function(
  configuration?: Configuration
) {
  return {
    /**
     *
     * @param {PostRequest} postRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsCreate(postRequest: PostRequest, options: any = {}): RequestArgs {
      // verify required parameter 'postRequest' is not null or undefined
      if (postRequest === null || postRequest === undefined) {
        throw new RequiredError(
          "postRequest",
          "Required parameter postRequest was null or undefined when calling postsCreate."
        );
      }
      const localVarPath = `/api/Posts`;
      const localVarUrlObj = url.parse(localVarPath, true);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }
      const localVarRequestOptions = Object.assign(
        { method: "POST" },
        baseOptions,
        options
      );
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication JWT required
      if (configuration && configuration.apiKey) {
        const localVarApiKeyValue =
          typeof configuration.apiKey === "function"
            ? configuration.apiKey("Authorization")
            : configuration.apiKey;
        localVarHeaderParameter["Authorization"] = localVarApiKeyValue;
      }

      localVarHeaderParameter["Content-Type"] = "application/json";

      localVarUrlObj.query = Object.assign(
        {},
        localVarUrlObj.query,
        localVarQueryParameter,
        options.query
      );
      // fix override query string Detail: https://stackoverflow.com/a/7517673/1077943
      delete localVarUrlObj.search;
      localVarRequestOptions.headers = Object.assign(
        {},
        localVarHeaderParameter,
        options.headers
      );
      const needsSerialization =
        localVarRequestOptions.headers["Content-Type"] === "application/json";
      localVarRequestOptions.data = needsSerialization
        ? JSON.stringify(postRequest || {})
        : postRequest || "";

      return {
        url: url.format(localVarUrlObj),
        options: localVarRequestOptions
      };
    },
    /**
     *
     * @param {number} id
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsDelete(id: number, options: any = {}): RequestArgs {
      // verify required parameter 'id' is not null or undefined
      if (id === null || id === undefined) {
        throw new RequiredError(
          "id",
          "Required parameter id was null or undefined when calling postsDelete."
        );
      }
      const localVarPath = `/api/Posts/{id}`.replace(
        `{${"id"}}`,
        encodeURIComponent(String(id))
      );
      const localVarUrlObj = url.parse(localVarPath, true);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }
      const localVarRequestOptions = Object.assign(
        { method: "DELETE" },
        baseOptions,
        options
      );
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication JWT required
      if (configuration && configuration.apiKey) {
        const localVarApiKeyValue =
          typeof configuration.apiKey === "function"
            ? configuration.apiKey("Authorization")
            : configuration.apiKey;
        localVarHeaderParameter["Authorization"] = localVarApiKeyValue;
      }

      localVarUrlObj.query = Object.assign(
        {},
        localVarUrlObj.query,
        localVarQueryParameter,
        options.query
      );
      // fix override query string Detail: https://stackoverflow.com/a/7517673/1077943
      delete localVarUrlObj.search;
      localVarRequestOptions.headers = Object.assign(
        {},
        localVarHeaderParameter,
        options.headers
      );

      return {
        url: url.format(localVarUrlObj),
        options: localVarRequestOptions
      };
    },
    /**
     *
     * @param {number} [page]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsGetAll(page?: number, options: any = {}): RequestArgs {
      const localVarPath = `/api/Posts`;
      const localVarUrlObj = url.parse(localVarPath, true);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }
      const localVarRequestOptions = Object.assign(
        { method: "GET" },
        baseOptions,
        options
      );
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication JWT required
      if (configuration && configuration.apiKey) {
        const localVarApiKeyValue =
          typeof configuration.apiKey === "function"
            ? configuration.apiKey("Authorization")
            : configuration.apiKey;
        localVarHeaderParameter["Authorization"] = localVarApiKeyValue;
      }

      if (page !== undefined) {
        localVarQueryParameter["page"] = page;
      }

      localVarUrlObj.query = Object.assign(
        {},
        localVarUrlObj.query,
        localVarQueryParameter,
        options.query
      );
      // fix override query string Detail: https://stackoverflow.com/a/7517673/1077943
      delete localVarUrlObj.search;
      localVarRequestOptions.headers = Object.assign(
        {},
        localVarHeaderParameter,
        options.headers
      );

      return {
        url: url.format(localVarUrlObj),
        options: localVarRequestOptions
      };
    },
    /**
     *
     * @param {number} id
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsGetById(id: number, options: any = {}): RequestArgs {
      // verify required parameter 'id' is not null or undefined
      if (id === null || id === undefined) {
        throw new RequiredError(
          "id",
          "Required parameter id was null or undefined when calling postsGetById."
        );
      }
      const localVarPath = `/api/Posts/{id}`.replace(
        `{${"id"}}`,
        encodeURIComponent(String(id))
      );
      const localVarUrlObj = url.parse(localVarPath, true);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }
      const localVarRequestOptions = Object.assign(
        { method: "GET" },
        baseOptions,
        options
      );
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication JWT required
      if (configuration && configuration.apiKey) {
        const localVarApiKeyValue =
          typeof configuration.apiKey === "function"
            ? configuration.apiKey("Authorization")
            : configuration.apiKey;
        localVarHeaderParameter["Authorization"] = localVarApiKeyValue;
      }

      localVarUrlObj.query = Object.assign(
        {},
        localVarUrlObj.query,
        localVarQueryParameter,
        options.query
      );
      // fix override query string Detail: https://stackoverflow.com/a/7517673/1077943
      delete localVarUrlObj.search;
      localVarRequestOptions.headers = Object.assign(
        {},
        localVarHeaderParameter,
        options.headers
      );

      return {
        url: url.format(localVarUrlObj),
        options: localVarRequestOptions
      };
    },
    /**
     *
     * @param {PreviewRequest} previewRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsPreview(
      previewRequest: PreviewRequest,
      options: any = {}
    ): RequestArgs {
      // verify required parameter 'previewRequest' is not null or undefined
      if (previewRequest === null || previewRequest === undefined) {
        throw new RequiredError(
          "previewRequest",
          "Required parameter previewRequest was null or undefined when calling postsPreview."
        );
      }
      const localVarPath = `/api/Posts/preview`;
      const localVarUrlObj = url.parse(localVarPath, true);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }
      const localVarRequestOptions = Object.assign(
        { method: "POST" },
        baseOptions,
        options
      );
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication JWT required
      if (configuration && configuration.apiKey) {
        const localVarApiKeyValue =
          typeof configuration.apiKey === "function"
            ? configuration.apiKey("Authorization")
            : configuration.apiKey;
        localVarHeaderParameter["Authorization"] = localVarApiKeyValue;
      }

      localVarHeaderParameter["Content-Type"] = "application/json";

      localVarUrlObj.query = Object.assign(
        {},
        localVarUrlObj.query,
        localVarQueryParameter,
        options.query
      );
      // fix override query string Detail: https://stackoverflow.com/a/7517673/1077943
      delete localVarUrlObj.search;
      localVarRequestOptions.headers = Object.assign(
        {},
        localVarHeaderParameter,
        options.headers
      );
      const needsSerialization =
        localVarRequestOptions.headers["Content-Type"] === "application/json";
      localVarRequestOptions.data = needsSerialization
        ? JSON.stringify(previewRequest || {})
        : previewRequest || "";

      return {
        url: url.format(localVarUrlObj),
        options: localVarRequestOptions
      };
    },
    /**
     *
     * @param {number} id
     * @param {PostRequest} postRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsUpdate(
      id: number,
      postRequest: PostRequest,
      options: any = {}
    ): RequestArgs {
      // verify required parameter 'id' is not null or undefined
      if (id === null || id === undefined) {
        throw new RequiredError(
          "id",
          "Required parameter id was null or undefined when calling postsUpdate."
        );
      }
      // verify required parameter 'postRequest' is not null or undefined
      if (postRequest === null || postRequest === undefined) {
        throw new RequiredError(
          "postRequest",
          "Required parameter postRequest was null or undefined when calling postsUpdate."
        );
      }
      const localVarPath = `/api/Posts/{id}`.replace(
        `{${"id"}}`,
        encodeURIComponent(String(id))
      );
      const localVarUrlObj = url.parse(localVarPath, true);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }
      const localVarRequestOptions = Object.assign(
        { method: "PUT" },
        baseOptions,
        options
      );
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication JWT required
      if (configuration && configuration.apiKey) {
        const localVarApiKeyValue =
          typeof configuration.apiKey === "function"
            ? configuration.apiKey("Authorization")
            : configuration.apiKey;
        localVarHeaderParameter["Authorization"] = localVarApiKeyValue;
      }

      localVarHeaderParameter["Content-Type"] = "application/json";

      localVarUrlObj.query = Object.assign(
        {},
        localVarUrlObj.query,
        localVarQueryParameter,
        options.query
      );
      // fix override query string Detail: https://stackoverflow.com/a/7517673/1077943
      delete localVarUrlObj.search;
      localVarRequestOptions.headers = Object.assign(
        {},
        localVarHeaderParameter,
        options.headers
      );
      const needsSerialization =
        localVarRequestOptions.headers["Content-Type"] === "application/json";
      localVarRequestOptions.data = needsSerialization
        ? JSON.stringify(postRequest || {})
        : postRequest || "";

      return {
        url: url.format(localVarUrlObj),
        options: localVarRequestOptions
      };
    }
  };
};

/**
 * PostsApi - functional programming interface
 * @export
 */
export const PostsApiFp = function(configuration?: Configuration) {
  return {
    /**
     *
     * @param {PostRequest} postRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsCreate(
      postRequest: PostRequest,
      options?: any
    ): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Response> {
      const localVarAxiosArgs = PostsApiAxiosParamCreator(
        configuration
      ).postsCreate(postRequest, options);
      return (
        axios: AxiosInstance = globalAxios,
        basePath: string = BASE_PATH
      ) => {
        const axiosRequestArgs = Object.assign(localVarAxiosArgs.options, {
          url: basePath + localVarAxiosArgs.url
        });
        return axios.request(axiosRequestArgs);
      };
    },
    /**
     *
     * @param {number} id
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsDelete(
      id: number,
      options?: any
    ): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Response> {
      const localVarAxiosArgs = PostsApiAxiosParamCreator(
        configuration
      ).postsDelete(id, options);
      return (
        axios: AxiosInstance = globalAxios,
        basePath: string = BASE_PATH
      ) => {
        const axiosRequestArgs = Object.assign(localVarAxiosArgs.options, {
          url: basePath + localVarAxiosArgs.url
        });
        return axios.request(axiosRequestArgs);
      };
    },
    /**
     *
     * @param {number} [page]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsGetAll(
      page?: number,
      options?: any
    ): (
      axios?: AxiosInstance,
      basePath?: string
    ) => AxiosPromise<Array<PostPreviewDto>> {
      const localVarAxiosArgs = PostsApiAxiosParamCreator(
        configuration
      ).postsGetAll(page, options);
      return (
        axios: AxiosInstance = globalAxios,
        basePath: string = BASE_PATH
      ) => {
        const axiosRequestArgs = Object.assign(localVarAxiosArgs.options, {
          url: basePath + localVarAxiosArgs.url
        });
        return axios.request(axiosRequestArgs);
      };
    },
    /**
     *
     * @param {number} id
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsGetById(
      id: number,
      options?: any
    ): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<PostDto> {
      const localVarAxiosArgs = PostsApiAxiosParamCreator(
        configuration
      ).postsGetById(id, options);
      return (
        axios: AxiosInstance = globalAxios,
        basePath: string = BASE_PATH
      ) => {
        const axiosRequestArgs = Object.assign(localVarAxiosArgs.options, {
          url: basePath + localVarAxiosArgs.url
        });
        return axios.request(axiosRequestArgs);
      };
    },
    /**
     *
     * @param {PreviewRequest} previewRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsPreview(
      previewRequest: PreviewRequest,
      options?: any
    ): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<PreviewDto> {
      const localVarAxiosArgs = PostsApiAxiosParamCreator(
        configuration
      ).postsPreview(previewRequest, options);
      return (
        axios: AxiosInstance = globalAxios,
        basePath: string = BASE_PATH
      ) => {
        const axiosRequestArgs = Object.assign(localVarAxiosArgs.options, {
          url: basePath + localVarAxiosArgs.url
        });
        return axios.request(axiosRequestArgs);
      };
    },
    /**
     *
     * @param {number} id
     * @param {PostRequest} postRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsUpdate(
      id: number,
      postRequest: PostRequest,
      options?: any
    ): (axios?: AxiosInstance, basePath?: string) => AxiosPromise<Response> {
      const localVarAxiosArgs = PostsApiAxiosParamCreator(
        configuration
      ).postsUpdate(id, postRequest, options);
      return (
        axios: AxiosInstance = globalAxios,
        basePath: string = BASE_PATH
      ) => {
        const axiosRequestArgs = Object.assign(localVarAxiosArgs.options, {
          url: basePath + localVarAxiosArgs.url
        });
        return axios.request(axiosRequestArgs);
      };
    }
  };
};

/**
 * PostsApi - factory interface
 * @export
 */
export const PostsApiFactory = function(
  configuration?: Configuration,
  basePath?: string,
  axios?: AxiosInstance
) {
  return {
    /**
     *
     * @param {PostRequest} postRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsCreate(postRequest: PostRequest, options?: any) {
      return PostsApiFp(configuration).postsCreate(postRequest, options)(
        axios,
        basePath
      );
    },
    /**
     *
     * @param {number} id
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsDelete(id: number, options?: any) {
      return PostsApiFp(configuration).postsDelete(id, options)(
        axios,
        basePath
      );
    },
    /**
     *
     * @param {number} [page]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsGetAll(page?: number, options?: any) {
      return PostsApiFp(configuration).postsGetAll(page, options)(
        axios,
        basePath
      );
    },
    /**
     *
     * @param {number} id
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsGetById(id: number, options?: any) {
      return PostsApiFp(configuration).postsGetById(id, options)(
        axios,
        basePath
      );
    },
    /**
     *
     * @param {PreviewRequest} previewRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsPreview(previewRequest: PreviewRequest, options?: any) {
      return PostsApiFp(configuration).postsPreview(previewRequest, options)(
        axios,
        basePath
      );
    },
    /**
     *
     * @param {number} id
     * @param {PostRequest} postRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    postsUpdate(id: number, postRequest: PostRequest, options?: any) {
      return PostsApiFp(configuration).postsUpdate(id, postRequest, options)(
        axios,
        basePath
      );
    }
  };
};

/**
 * PostsApi - object-oriented interface
 * @export
 * @class PostsApi
 * @extends {BaseAPI}
 */
export class PostsApi extends BaseAPI {
  /**
   *
   * @param {PostRequest} postRequest
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof PostsApi
   */
  public postsCreate(postRequest: PostRequest, options?: any) {
    return PostsApiFp(this.configuration).postsCreate(postRequest, options)(
      this.axios,
      this.basePath
    );
  }

  /**
   *
   * @param {number} id
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof PostsApi
   */
  public postsDelete(id: number, options?: any) {
    return PostsApiFp(this.configuration).postsDelete(id, options)(
      this.axios,
      this.basePath
    );
  }

  /**
   *
   * @param {number} [page]
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof PostsApi
   */
  public postsGetAll(page?: number, options?: any) {
    return PostsApiFp(this.configuration).postsGetAll(page, options)(
      this.axios,
      this.basePath
    );
  }

  /**
   *
   * @param {number} id
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof PostsApi
   */
  public postsGetById(id: number, options?: any) {
    return PostsApiFp(this.configuration).postsGetById(id, options)(
      this.axios,
      this.basePath
    );
  }

  /**
   *
   * @param {PreviewRequest} previewRequest
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof PostsApi
   */
  public postsPreview(previewRequest: PreviewRequest, options?: any) {
    return PostsApiFp(this.configuration).postsPreview(previewRequest, options)(
      this.axios,
      this.basePath
    );
  }

  /**
   *
   * @param {number} id
   * @param {PostRequest} postRequest
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof PostsApi
   */
  public postsUpdate(id: number, postRequest: PostRequest, options?: any) {
    return PostsApiFp(this.configuration).postsUpdate(id, postRequest, options)(
      this.axios,
      this.basePath
    );
  }
}
