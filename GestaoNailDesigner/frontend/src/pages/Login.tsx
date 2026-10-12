import { useState, type FormEvent } from 'react';
import { api } from '../services/api';
import '../Login.css';

export function Login() {
    const [email, setEmail] = useState('');
    const [senha, setSenha] = useState('');
    const [mostrarSenha, setMostrarSenha] = useState(false);
    const [carregando, setCarregando] = useState(false);
    const [mensagem, setMensagem] = useState('');
    const [erro, setErro] = useState(false);

    async function handleLogin(e: FormEvent) {
        e.preventDefault();
        setCarregando(true);
        setMensagem('');

        try {
            const response = await api.post('/Login', { email, senha });
            localStorage.setItem('token', response.data.token);
            setErro(false);
            setMensagem(response.data.mensagem);
        } catch (err: unknown) {
            const error = err as { response?: { data?: { mensagem?: string } } };
            setErro(true);
            setMensagem(error.response?.data?.mensagem ?? 'Erro de conexão com o servidor.');
        } finally {
            setCarregando(false);
        }
    }

    return (
        <div className="login-page">
            <form className="login-card" onSubmit={handleLogin}>

                <h1 className="login-title">Login</h1>

                <div className="login-field">
                    <label className="login-label" htmlFor="email">Usuário</label>
                    <input
                        id="email"
                        className="login-input"
                        type="email"
                        placeholder="seuemail@exemplo.com"
                        autoComplete="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </div>

                <div className="login-field">
                    <label className="login-label" htmlFor="senha">Senha</label>
                    <div className="login-password">
                        <input
                            id="senha"
                            className="login-input"
                            type={mostrarSenha ? 'text' : 'password'}
                            placeholder="Digite sua senha"
                            autoComplete="current-password"
                            value={senha}
                            onChange={(e) => setSenha(e.target.value)}
                            required
                        />
                        <button
                            type="button"
                            className="login-toggle"
                            onClick={() => setMostrarSenha((v) => !v)}
                            aria-label={mostrarSenha ? 'Ocultar senha' : 'Mostrar senha'}
                        >
                            {mostrarSenha ? 'Ocultar' : 'Mostrar'}
                        </button>
                    </div>
                </div>

                <button className="login-button" type="submit" disabled={carregando}>
                    {carregando ? 'Entrando...' : 'Entrar'}
                </button>

                {mensagem && (
                    <p
                        className={`login-message ${erro ? 'login-message--error' : 'login-message--success'}`}
                        role="alert"
                    >
                        {mensagem}
                    </p>
                )}
            </form>
        </div>
    );
}