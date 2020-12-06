export class LocalStorageUtils {
    
    public obterUsuario() {
        return JSON.parse(localStorage.getItem('geekstore.user'));
    }

    public salvarDadosLocaisUsuario(response: any) {
        this.salvarTokenUsuario(response.accessToken);
        this.salvarUsuario(response.userToken);
    }

    public limparDadosLocaisUsuario() {
        localStorage.removeItem('geekstore.token');
        localStorage.removeItem('geekstore.user');
    }

    public obterTokenUsuario(): string {
        return localStorage.getItem('geekstore.token');
    }

    public salvarTokenUsuario(token: string) {
        localStorage.setItem('geekstore.token', token);
    }

    public salvarUsuario(user: string) {
        localStorage.setItem('geekstore.user', JSON.stringify(user));
    }
}
