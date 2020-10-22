using System;
using Interface;
using Manager;
using Model;
using UnityEngine;
using VIew;


namespace Controller
{
    public sealed class ZoneController : IEnabled
    {
        #region Fields

        private GameContext _context;
        private Services _services;
        private WaterZoneView _waterZoneView;

        #endregion

        public ZoneController(Services services, GameContext context)
        {
            _services = services;
            _context = context;
            _waterZoneView = _context.WaterZone.GetComponent<WaterZoneView>();
        }

        public void On()
        {
            if (_waterZoneView == null) return;
            _waterZoneView.OnEnter += ToSwim;
            _waterZoneView.OnExit += UnSwim;
        }

        public void Off()
        {
            if (_waterZoneView == null) return;
            _waterZoneView.OnEnter -= ToSwim;
            _waterZoneView.OnExit -= UnSwim;
        }

        private void UnSwim(Collider obj)
        {
            if(!Helper.CheckForComparerLayer(_context.LayerUnits, obj)) return;
            obj.GetComponent<BaseUnitView>().ToUnSwim();
        }

        private void ToSwim(Collider obj)
        {
            if(!Helper.CheckForComparerLayer(_context.LayerUnits, obj)) return;
            obj.GetComponent<BaseUnitView>().ToSwim();
        }
    }
}