export interface AuthState {
  isUserLoggedIn: boolean,
  accessToken?: string,
  roles?: string[],
  isFirstLogginAttempt: boolean
}

export interface AccessToken{
  accessToken: string
}
