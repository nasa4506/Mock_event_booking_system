export interface User {
  id: string;
  email: string;
  name: string;
  role: string;
  avatarUrl?: string;
}

export interface AuthResponse {
  token: string;
  user: User;
}
