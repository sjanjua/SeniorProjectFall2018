package com.seniorproject.sarimjanjua.inventorymanagementsystem

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.view.Menu
import android.view.MenuInflater

class HomePage : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_home_page)
        setSupportActionBar( findViewById( R.id.toolbar ) )
    }

    override fun onCreateOptionsMenu(menu : Menu?) : Boolean
    {
        var inflater : MenuInflater = menuInflater
        inflater.inflate( R.menu.homepage_menu, menu )
        
        return super.onCreateOptionsMenu(menu)
    }
}
