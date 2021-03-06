using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrganizerU.Interfaces;
using OrganizerU.Models;

namespace OrganizerU.Controllers {
    [Authorize]
    [Route ("[controller]/{UserId}")]
    public class EstudianteController : Controller {
        private readonly IEstudiante _estudiante;

        public EstudianteController (IEstudiante _estudiante) => this._estudiante = _estudiante;

        [HttpGet]
        public Task<IActionResult> Get (string UserId) => GET (UserId);

        private async Task<IActionResult> GET (string UserId) {
            try {
                if (string.IsNullOrEmpty (UserId) || UserId.Length < 24) {
                    return StatusCode (StatusCodes.Status406NotAcceptable,"Id Invalid");
                }
                Estudiante  es  =await _estudiante.Get (UserId);
                if (es == null) {
                    return StatusCode (StatusCodes.Status406NotAcceptable,"No Hay Documentos");
                } else {
                    return Ok (JsonConvert.SerializeObject (es));
                }
            } catch (Exception) {
                return BadRequest ("Ha Ocurrido Un Error Vuelva A Intentar");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put ([FromBody] Estudiante put, string UserId) {
            try {
                if (string.IsNullOrEmpty (UserId) || UserId.Length < 24) {
                    return StatusCode (StatusCodes.Status406NotAcceptable,"Id Invalid");
                }
                if (ModelState.IsValid) {
                    put.Id = UserId;
                    var h = await _estudiante.Update (UserId, put);
                    if (h.MatchedCount > 0) {
                        return Ok ("Modificado");
                    } else {
                        return BadRequest ("Hubo un error");
                    }
                } else {
                    return StatusCode (StatusCodes.Status406NotAcceptable,ModelState);
                }
            } catch (Exception) {
                return BadRequest ("Ha Ocurrido Un Error Vuelva A Intentar");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete (string UserId) {
            try {
                if (string.IsNullOrEmpty (UserId) || UserId.Length < 24) {
                    return StatusCode (StatusCodes.Status406NotAcceptable,"Id Invalid");
                }
                var e = await _estudiante.Remove (UserId);
                if (e.DeletedCount > 0) {
                    return Ok ("Eliminado");
                } else {
                    return StatusCode (StatusCodes.Status406NotAcceptable,"Hubo un error");
                }
            } catch (Exception) {
                return BadRequest ("Ha Ocurrido Un Error Vuelva A Intentar");
            }
        }
    }
}