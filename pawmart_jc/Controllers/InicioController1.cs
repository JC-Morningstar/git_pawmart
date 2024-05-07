using Microsoft.AspNetCore.Mvc;
using pawmart_jc.Models;
using pawmart_jc.Recursos;
using pawmart_jc.Servicios.Contrato;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace pawmart_jc.Controllers
{
    public class InicioController1 : Controller
    {
        private readonly IUsuarioService _usuarioServicio;

        public InicioController1(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Cliente modelo)
        {
            if (modelo.Contraseña != null)
            {
                modelo.Contraseña = Utilidades.EncriptarContraseña(modelo.Contraseña);
            }
            else
            {
                ViewData["Mensaje"] = "La contraseña no puede ser nula.";
                return View();
            }

            Cliente cliente_creado = await _usuarioServicio.SaveCliente(modelo);

            if (cliente_creado.Id > 0)
            {
                return RedirectToAction("IniciarSeccion", "InicioController1");
            }
            else
            {
                ViewData["Mensaje"] = "No se pudo crear el usuario.";
                return View();
            }
        }

        public IActionResult IniciarSeccion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSeccion(string CorreoElectronico, string Contraseña)
        {
            if (CorreoElectronico == null || Contraseña == null)
            {
                ViewData["Mensaje"] = "El correo electrónico y la contraseña no pueden ser nulos.";
                return View();
            }

            string contraseñaEncriptada = Utilidades.EncriptarContraseña(Contraseña);
            Cliente cliente_encontrado = await _usuarioServicio.GetCliente(CorreoElectronico, contraseñaEncriptada);

            if (cliente_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias.";
                return View();
            }

            // Comprobación de nulidad antes de crear la instancia de Claim
            List<Claim> claims = new List<Claim>();
            if (cliente_encontrado.Nombre != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, cliente_encontrado.Nombre));
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),
                properties);

            return RedirectToAction("Index", "Home");
        }
    }
}
